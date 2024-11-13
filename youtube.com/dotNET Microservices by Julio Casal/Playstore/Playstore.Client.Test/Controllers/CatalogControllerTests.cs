using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Playstore.Client.ServiceClients;

namespace Playstore.Client.Test.Controllers;

[TestClass]
public class CatalogControllerTests
{
    private Mock<ICatalogClient> _catalogClientMock;

    private CatalogController _catalogController;

    [TestInitialize]
    public void TestInitialize()
    {
        _catalogClientMock = new Mock<ICatalogClient>();
        _catalogController = new CatalogController(_catalogClientMock.Object);

        var tempDataProviderMock = new Mock<ITempDataProvider>();
        var tempDataDictionary = new TempDataDictionary(new DefaultHttpContext(), tempDataProviderMock.Object);
        _catalogController.TempData = tempDataDictionary;
    }

    [TestMethod]
    public async Task IndexAsync_ReturnsView()
    {
        // Arrange
        var expectedItem = new CatalogItemDto(Guid.NewGuid(), "item name", "item description", 4.5m, DateTimeOffset.UtcNow);

        _catalogClientMock.Setup(m => m.GetAllItemsAsync()).ReturnsAsync(new List<CatalogItemDto> { expectedItem });

        // Act
        IActionResult result = await _catalogController.IndexAsync();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        var model = (IEnumerable<CatalogItemViewModel>)viewResult.Model;
        var actualItem = model.Single();

        Assert.AreEqual(expectedItem.Id, actualItem.Id);
        Assert.AreEqual(expectedItem.Name, actualItem.Name);
        Assert.AreEqual(expectedItem.Description, actualItem.Description);
        Assert.AreEqual(expectedItem.Price, actualItem.Price);
    }

    [TestMethod]
    public void Create_ReturnsView()
    {
        // Act
        IActionResult result = _catalogController.Create();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public async Task CreateAsync_CreatesItem()
    {
        // Arrange
        var expectedItem = new CatalogItemViewModel
        {
            Name = "test name",
            Description = "test description",
            Price = 4.5m
        };

        // Act
        IActionResult result = await _catalogController.CreateAsync(expectedItem);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectResult.ActionName);

        _catalogClientMock.Verify(m => m.CreateItemAsync(It.Is<CreateCatalogItemDto>(
            i => i.Name == expectedItem.Name
            && i.Description == expectedItem.Description
            && i.Price == expectedItem.Price
        )));

        Assert.IsTrue(_catalogController.TempData.ContainsKey(NotificationsViewModel.c_Success));
    }

    [TestMethod]
    public async Task CreateAsync_ReturnsViewIfErrors()
    {
        // Arrange
        var expectedItem = new CatalogItemViewModel();

        _catalogController.ModelState.AddModelError("test", "test error");

        // Act
        IActionResult result = await _catalogController.CreateAsync(expectedItem);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;

        Assert.AreSame(expectedItem, viewResult.Model);
    }

    [TestMethod]
    public async Task EditAsync_ReturnsView()
    {
        // Arrange
        var expectedGuid = Guid.NewGuid();
        var expectedItem = new CatalogItemDto(expectedGuid, "item name", "item description", 4.5m, DateTimeOffset.UtcNow);

        _catalogClientMock.Setup(m => m.GetItemAsync(expectedGuid)).ReturnsAsync(expectedItem);

        // Act
        IActionResult result = await _catalogController.EditAsync(expectedGuid);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        var actualItem = (CatalogItemViewModel)viewResult.Model;

        Assert.AreEqual(expectedGuid, actualItem.Id);
        Assert.AreEqual(expectedItem.Name, actualItem.Name);
        Assert.AreEqual(expectedItem.Description, actualItem.Description);
        Assert.AreEqual(expectedItem.Price, actualItem.Price);
    }

    [TestMethod]
    public async Task EditAsync_ReturnsNotFoundIfNoId()
    {
        // Act
        IActionResult result = await _catalogController.EditAsync(null as Guid?);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task EditAsync_ReturnsNotFoundIf404Error()
    {
        // Arrange
        var testId = Guid.NewGuid();

        _catalogClientMock.Setup(m => m.GetItemAsync(testId))
            .ThrowsAsync(new HttpRequestException(string.Empty, null, HttpStatusCode.NotFound));

        // Act
        IActionResult result = await _catalogController.EditAsync(testId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task EditAsync_RedirectsToIndexifBadResponse()
    {
        // Arrange
        var testId = Guid.NewGuid();

        _catalogClientMock.Setup(m => m.GetItemAsync(testId))
            .ThrowsAsync(new HttpRequestException());

        // Act
        IActionResult result = await _catalogController.EditAsync(testId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectResult.ActionName);

        Assert.IsTrue(_catalogController.TempData.ContainsKey(NotificationsViewModel.c_Error));
    }

    [TestMethod]
    public async Task EditAsync_UpdatesItem()
    {
        // Arrange
        var expectedItem = new CatalogItemViewModel
        {
            Id = Guid.NewGuid(),
            Name = "item name",
            Description = "item description",
            Price = 4.5m
        };

        // Act
        var result = await _catalogController.EditAsync(expectedItem);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectResult.ActionName);

        _catalogClientMock.Verify(m => m.UpdateItemAsync(
            It.Is<Guid>(id => id == expectedItem.Id),
            It.Is<UpdateCatalogItemDto>(
                i => i.Name == expectedItem.Name
                && i.Description == expectedItem.Description
                && i.Price == expectedItem.Price
            )));

        Assert.IsTrue(_catalogController.TempData.ContainsKey(NotificationsViewModel.c_Success));
    }

    [TestMethod]
    public async Task EditAsync_ReturnsViewIfErrors()
    {
        // Arrange
        var expectedItem = new CatalogItemViewModel();

        _catalogController.ModelState.AddModelError("test", "test error");

        // Act
        IActionResult result = await _catalogController.EditAsync(expectedItem);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;

        Assert.AreSame(expectedItem, viewResult.Model);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnsView()
    {
        // Arrange
        var expectedGuid = Guid.NewGuid();
        var expectedItem = new CatalogItemDto(expectedGuid, "item name", "item description", 4.5m, DateTimeOffset.UtcNow);

        _catalogClientMock.Setup(m => m.GetItemAsync(expectedGuid)).ReturnsAsync(expectedItem);

        // Act
        IActionResult result = await _catalogController.DeleteAsync(expectedGuid);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        var actualItem = (CatalogItemViewModel)viewResult.Model;

        Assert.AreEqual(expectedGuid, actualItem.Id);
        Assert.AreEqual(expectedItem.Name, actualItem.Name);
        Assert.AreEqual(expectedItem.Description, actualItem.Description);
        Assert.AreEqual(expectedItem.Price, actualItem.Price);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnsNotFoundIfNoId()
    {
        // Act
        IActionResult result = await _catalogController.DeleteAsync(null as Guid?);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnsNotFoundIf404Error()
    {
        // Arrange
        var testId = Guid.NewGuid();

        _catalogClientMock.Setup(m => m.GetItemAsync(testId))
            .ThrowsAsync(new HttpRequestException(string.Empty, null, HttpStatusCode.NotFound));

        // Act
        IActionResult result = await _catalogController.DeleteAsync(testId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteAsync_RedirectsToIndexifBadResponse()
    {
        // Arrange
        var testId = Guid.NewGuid();

        _catalogClientMock.Setup(m => m.GetItemAsync(testId))
            .ThrowsAsync(new HttpRequestException());

        // Act
        IActionResult result = await _catalogController.DeleteAsync(testId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectResult.ActionName);

        Assert.IsTrue(_catalogController.TempData.ContainsKey(NotificationsViewModel.c_Error));
    }

    [TestMethod]
    public async Task DeleteAsync_UpdatesItem()
    {
        // Arrange
        var expectedItemId = Guid.NewGuid();

        // Act
        var result = await _catalogController.DeleteConfirmAsync(expectedItemId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectResult.ActionName);

        _catalogClientMock.Verify(m => m.DeleteItemAsync(
            It.Is<Guid>(id => id == expectedItemId)));

        Assert.IsTrue(_catalogController.TempData.ContainsKey(NotificationsViewModel.c_Success));
    }
}