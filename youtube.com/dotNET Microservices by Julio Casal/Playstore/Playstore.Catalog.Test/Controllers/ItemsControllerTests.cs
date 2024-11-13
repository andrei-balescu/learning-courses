using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Catalog.Service.Controllers;
using Playstore.Catalog.Service.Entities;
using Playstore.Common;

namespace Playstore.Catalog.Test.Controllers;

[TestClass]
public class ItemsControllerTests
{
    private ItemsController _itemsController;

    private Mock<IRepository<Item>> _itemRepositoryMock;

    private Mock<IPublishEndpoint> _publishEndpointMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _itemRepositoryMock = new Mock<IRepository<Item>>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();

        _itemsController = new ItemsController(_itemRepositoryMock.Object, _publishEndpointMock.Object);
    }

    [TestMethod]
    public async Task GetAsync_GetsItemsFromDb()
    {
        // Arrange
        Guid expectedItemId = Guid.NewGuid();
        Item dbItem = new Item { Id = expectedItemId };
        _itemRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(new List<Item> { dbItem });

        // Act
        IEnumerable<CatalogItemDto> result = await _itemsController.GetAsync();

        // Assert
        CatalogItemDto actualItem = result.Single();

        Assert.AreEqual(expectedItemId, actualItem.Id);
    }

    [TestMethod]
    public async Task GetByIdAsync_GetsItemFromDb()
    {
        // Arrange
        Guid expectedItemId = Guid.NewGuid();
        Item dbItem = new Item { Id = expectedItemId };
        _itemRepositoryMock.Setup(m => m.GetAsync(expectedItemId)).ReturnsAsync(new Item { Id = expectedItemId });

        // Act
        ActionResult<CatalogItemDto> actualItem = await _itemsController.GetByIdAsync(expectedItemId);

        // Assert
        Assert.AreEqual(expectedItemId, actualItem.Value.Id);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsNotFoundIfItemDoesNotExist()
    {
        // Arrange
        _itemRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Guid>())).ReturnsAsync(null as Item);

        // Act
        ActionResult<CatalogItemDto> actualItem = await _itemsController.GetByIdAsync(new Guid());

        // Assert
        Assert.IsInstanceOfType(actualItem.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task CreateAsync_UpdatesDbAndCallsPublishEndpoint()
    {
        // Arrange
        string expectedName = "expected name";
        string expectedDescription = "expected description";
        decimal expectedPrice = 4.5m;
        DateTimeOffset expectedCreatedDate = DateTimeOffset.UtcNow;

        CreateCatalogItemDto dto = new (expectedName, expectedDescription, expectedPrice);

        // Act
        await _itemsController.CreateAsync(dto);

        // Assert
        _itemRepositoryMock.Verify(m => m.CreateAsync(It.Is<Item>(
            i => i.Name == expectedName 
            && i.Description == expectedDescription
            && i.Price == expectedPrice
            && i.Id != Guid.Empty
            && (expectedCreatedDate - i.CreatedDate).Milliseconds < 1)));

        _publishEndpointMock.Verify(m => m.Publish(It.Is<CatalogItemCreated>(
            i => i.Name == expectedName
            && i.Description == expectedDescription
        ), It.IsAny<CancellationToken>()));
    }

    [TestMethod]
    public async Task CreateAsync_ReturnsCreatedAtResult()
    {
        // Arrange
        string expectedName = "expected name";
        string expectedDescription = "expected description";
        decimal expectedPrice = 4.5m;
        DateTimeOffset expectedCreatedDate = DateTimeOffset.UtcNow;

        CreateCatalogItemDto dto = new(expectedName, expectedDescription, expectedPrice);

        // Act
        ActionResult<CatalogItemDto> result = await _itemsController.CreateAsync(dto);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        var actionResult = (CreatedAtActionResult)result.Result;

        Assert.AreEqual("GetByIdAsync", actionResult.ActionName);
        var actualValue = (CatalogItemDto)actionResult.Value;
        Assert.AreEqual(actionResult.RouteValues["Id"], actualValue.Id);
        Assert.AreEqual(expectedName, actualValue.Name);
        Assert.AreEqual(expectedDescription, actualValue.Description);
        Assert.AreEqual(expectedPrice, actualValue.Price);
    }

    [TestMethod]
    public async Task UpdateAsync_UpdatesDbAndCallsPublishEndpoint()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        string expectedName = "expected name";
        string expectedDescription = "expected description";
        decimal expectedPrice = 4.5m;

        _itemRepositoryMock.Setup(m => m.GetAsync(expectedId)).ReturnsAsync(new Item { Id = expectedId });

        // Act
        IActionResult result = await _itemsController.UpdateAsync(expectedId, new UpdateCatalogItemDto(expectedName, expectedDescription, expectedPrice));

        // Assert
        _itemRepositoryMock.Verify(m => m.UpdateAsync(It.Is<Item>(
            i => i.Name == expectedName
            && i.Description == expectedDescription
            && i.Price == expectedPrice)));

        _publishEndpointMock.Verify(m => m.Publish(It.Is<CatalogItemUpdated>(
            i => i.ItemId == expectedId
            && i.Name == expectedName
            && i.Description == expectedDescription
        ), It.IsAny<CancellationToken>()));

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotFoundIfItemDoesNotExist()
    {
        // Arrange
        _itemRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Guid>())).ReturnsAsync(null as Item);

        // Act
        IActionResult result = await _itemsController.UpdateAsync(new Guid(), null);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteAsync_UpdatesDbAndCallsPublishEndpoint()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();

        _itemRepositoryMock.Setup(m => m.GetAsync(expectedId)).ReturnsAsync(new Item());

        // Act
        IActionResult result = await _itemsController.DeleteAsync(expectedId);

        // Assert
        _itemRepositoryMock.Verify(m => m.RemoveAsync(expectedId));
        _publishEndpointMock.Verify(m => m.Publish(It.Is<CatalogItemDeleted>(i => i.ItemId == expectedId), It.IsAny<CancellationToken>()));

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnsNotFoundIfItemDoesNotExist()
    {
        // Arrange
        _itemRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Guid>())).ReturnsAsync(null as Item);

        // Act
        IActionResult result = await _itemsController.DeleteAsync(new Guid());

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}