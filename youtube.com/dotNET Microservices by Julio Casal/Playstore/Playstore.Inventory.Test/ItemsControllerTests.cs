using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Common;
using Playstore.Inventory.Contracts.DataTransferObjects;
using Playstore.Inventory.Service.Controllers;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Test;

[TestClass]
public class ItemsControllerTests
{
    private Mock<IRepository<InventoryItem>> _inventoryRepositoryMock;
    private Mock<IRepository<CatalogItem>> _catalogRepositoryMock;

    private ItemsController _itemsController;

    [TestInitialize]
    public void TestInitialize()
    {
        _inventoryRepositoryMock = new Mock<IRepository<InventoryItem>>();
        _catalogRepositoryMock = new Mock<IRepository<CatalogItem>>();

        _itemsController = new ItemsController(_inventoryRepositoryMock.Object, _catalogRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetAsync_ReturnsItems()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid expectedCatalogItemId = Guid.NewGuid();
        string expectedName = "expected name";
        string expectedDescription = "expected description";
        int expectedQuantity = 5;

        _inventoryRepositoryMock.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<InventoryItem, bool>>>())).ReturnsAsync(
            (Expression<Func<InventoryItem, bool>> predicate) =>
            {
                List<InventoryItem> itemsInDb = new ()
                {
                    new InventoryItem
                    {
                        UserId = userId,
                        CatalogItemId = expectedCatalogItemId,
                        Quantity = expectedQuantity,
                    }
                };
                
                Func<InventoryItem, bool> predicateFunction = predicate.Compile();
                IReadOnlyCollection<InventoryItem> returnItems = itemsInDb.Where(predicateFunction).ToList().AsReadOnly();
                return returnItems;
            });

        _catalogRepositoryMock.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<CatalogItem, bool>>>())).ReturnsAsync(
            (Expression<Func<CatalogItem, bool>> predicate) =>
            {
                List<CatalogItem> itemsInDb = new()
                {
                    new CatalogItem
                    {
                        Id = expectedCatalogItemId,
                        Name = expectedName,
                        Description = expectedDescription
                    }
                };
                
                Func<CatalogItem, bool> predicateFunction = predicate.Compile();
                IReadOnlyCollection<CatalogItem> returnItems = itemsInDb.Where(predicateFunction).ToList().AsReadOnly();
                return returnItems;
            });

        // Act
        ActionResult<IEnumerable<InventoryItemDto>> result = await _itemsController.GetAsync(userId);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result.Result;
        var resultValue = (IEnumerable<InventoryItemDto>)okResult.Value;
        InventoryItemDto actualItem = resultValue.Single();

        Assert.AreEqual(expectedCatalogItemId, actualItem.CatalogItemId);
        Assert.AreEqual(expectedName, actualItem.Name);
        Assert.AreEqual(expectedDescription, actualItem.Description);
        Assert.AreEqual(expectedQuantity, actualItem.Quantity);
    }

    [TestMethod]
    public async Task GetAsync_BadRequestifGuidEmpty()
    {
        // Arrange & Act
        ActionResult<IEnumerable<InventoryItemDto>> result = await _itemsController.GetAsync(Guid.Empty);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task GrantItemsAsync_UpdatesItem()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid catalogItemId = Guid.NewGuid();
        int initialQuantity = 5;
        int purchaseQuantity = 7;
        int expectedQuantity = initialQuantity + purchaseQuantity;

        _inventoryRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<InventoryItem, bool>>>())).ReturnsAsync(
            (Expression<Func<InventoryItem, bool>> predicate) =>
            {
                List<InventoryItem> itemsInDb = new ()
                {
                    new InventoryItem
                    {
                        UserId = userId,
                        CatalogItemId = catalogItemId,
                        Quantity = initialQuantity,
                    }
                };
                
                Func<InventoryItem, bool> predicateFunction = predicate.Compile();
                InventoryItem returnItem = itemsInDb.Where(predicateFunction).SingleOrDefault();
                return returnItem;
            });

        // Act
        await _itemsController.GrantItemsAsync(new GrantInventoryItemsDto(userId, catalogItemId, purchaseQuantity));

        // Assert
        _inventoryRepositoryMock.Verify(m => m.UpdateAsync(It.Is<InventoryItem>(i => i.Quantity == expectedQuantity)));
    }

    [TestMethod]
    public async Task GrantItemsAsync_CreatesNewItemIfNotFound()
    {
        // Arrange
        Guid expectedUserId = Guid.NewGuid();
        Guid expectedCatalogItemId = Guid.NewGuid();
        int expectedQuantity = 5;
        DateTimeOffset expectedAcquiredDate = DateTimeOffset.UtcNow;

        _inventoryRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<InventoryItem, bool>>>())).ReturnsAsync(null as InventoryItem);

        // Act
        await _itemsController.GrantItemsAsync(new GrantInventoryItemsDto(expectedUserId, expectedCatalogItemId, expectedQuantity));

        // Assert
        _inventoryRepositoryMock.Verify(m => m.CreateAsync(It.Is<InventoryItem>(
            i => i.Id != Guid.Empty
            && i.UserId == expectedUserId
            && i.CatalogItemId == expectedCatalogItemId
            && i.Quantity == expectedQuantity
            && (expectedAcquiredDate - i.AcquiredDate).Milliseconds < 1)));
    }
}