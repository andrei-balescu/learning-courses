using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Client.Controllers;
using Playstore.Client.Models;
using Playstore.Client.Models.Catalog;
using Playstore.Client.Models.Inventory;
using Playstore.Client.ServiceClients;
using Playstore.Inventory.Contracts.DataTransferObjects;

namespace Playstore.Client.Test.Controllers;

[TestClass]
public class InventoryControllerTests
{
    private Mock<IInventoryClient> _inventoryClientMock;
    private Mock<ICatalogClient> _catalogClientMock;

    private InventoryController _inventoryController;

    [TestInitialize]
    public void TestInitialize()
    {
        _inventoryClientMock = new Mock<IInventoryClient>();
        _catalogClientMock = new Mock<ICatalogClient>();
        _inventoryController = new InventoryController(_inventoryClientMock.Object, _catalogClientMock.Object);

        var tempDataProviderMock = new Mock<ITempDataProvider>();
        var tempDataDictionary = new TempDataDictionary(new DefaultHttpContext(), tempDataProviderMock.Object);
        _inventoryController.TempData = tempDataDictionary;
    }

    [TestMethod]
    public async Task Index_ReturnsItems()
    {
        // Arrange
        var expectedItem = new InventoryItemDto(Guid.NewGuid(), "item name", "item desctiption", 3, DateTimeOffset.UtcNow);

        var userId = Guid.NewGuid();
        var subjectClaim = new Claim(JwtRegisteredClaimNames.Sub, userId.ToString());
        SetupUserClaims(new List<Claim> { subjectClaim });

        _inventoryClientMock.Setup(m => m.GetItemsAsync(userId)).ReturnsAsync(new List<InventoryItemDto> { expectedItem });

        // Act
        IActionResult result = await _inventoryController.IndexAsync();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        var model = (IEnumerable<InventoryItemViewModel>)viewResult.Model;
        InventoryItemViewModel actualItem = model.Single();

        Assert.AreEqual(expectedItem.CatalogItemId, actualItem.Id);
        Assert.AreEqual(expectedItem.Name, actualItem.Name);
        Assert.AreEqual(expectedItem.Description, actualItem.Description);
        Assert.AreEqual(expectedItem.Quantity, actualItem.Quantity);
    }

    [TestMethod]
    public async Task PurchaseAsync_ReturnsCatalogItems()
    {
        // Arrange
        var expectedItem = new CatalogItemDto(Guid.NewGuid(), "test name", "test description", 4.5m, DateTimeOffset.UtcNow);
        _catalogClientMock.Setup(m => m.GetAllItemsAsync()).ReturnsAsync(new List<CatalogItemDto> { expectedItem });

        // Act
        var result = await _inventoryController.PurchaseAsync();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        var model = (IEnumerable<CatalogItemViewModel>)viewResult.Model;
        CatalogItemViewModel actualItem = model.Single();

        Assert.AreEqual(expectedItem.Id, actualItem.Id);
        Assert.AreEqual(expectedItem.Name, actualItem.Name);
        Assert.AreEqual(expectedItem.Description, actualItem.Description);
        Assert.AreEqual(expectedItem.Price, actualItem.Price);
    }

    [TestMethod]
    public async Task PurchaseItemAsync_ReturnsItem()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var expectedItem = new CatalogItemDto(itemId, "test name", "test description", 4.5m, DateTimeOffset.UtcNow);
        _catalogClientMock.Setup(m => m.GetItemAsync(itemId)).ReturnsAsync(expectedItem);

        // Act
        var result = await _inventoryController.PurchaseItemAsync(itemId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        var actualItem = (CatalogItemViewModel)viewResult.Model;

        Assert.AreEqual(expectedItem.Id, actualItem.Id);
        Assert.AreEqual(expectedItem.Name, actualItem.Name);
        Assert.AreEqual(expectedItem.Description, actualItem.Description);
        Assert.AreEqual(expectedItem.Price, actualItem.Price);
    }

    [TestMethod]
    public async Task PurchaseItemAsync_NotFoundIfItemNull()
    {
        // Act
        var result = await _inventoryController.PurchaseItemAsync(null as Guid?);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PurchaseItemAsync_NotFound404Exception()
    {
        // Arrange
        _catalogClientMock.Setup(m => m.GetItemAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new HttpRequestException(string.Empty, null, HttpStatusCode.NotFound));

        // Act
        var result = await _inventoryController.PurchaseItemAsync(null as Guid?);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PurchaseItemAsync_CallsPurchaseEndpoint()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        var subjectClaim = new Claim(JwtRegisteredClaimNames.Sub, expectedId.ToString());
        SetupUserClaims(new List<Claim> { subjectClaim });

        var expectedItem = new PurchaseItemViewModel
        {
            Id = Guid.NewGuid(),
            Quantity = 3
        };

        // Act
        await _inventoryController.PurchaseItemAsync(expectedItem);

        // Assert
        _inventoryClientMock.Verify(m => m.GrantInventoryItems(It.Is<GrantInventoryItemsDto>(
            dto => dto.UserId == expectedId
            && dto.CatalogItemId == expectedItem.Id
            && dto.Quantity == expectedItem.Quantity)));

        Assert.IsTrue(_inventoryController.TempData.ContainsKey(NotificationsViewModel.c_Success));
    }

    private void SetupUserClaims(IEnumerable<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.SetupGet(m => m.User).Returns(claimsPrincipal);

        var controllerContext = new ControllerContext();
        controllerContext.HttpContext = httpContextMock.Object;

        _inventoryController.ControllerContext = controllerContext;
    }
}