using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Common;
using Playstore.Inventory.Service.Consumers;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Test.Consumers;

[TestClass]
public class CatalogItemUpdateConsumerTests
{
    private Mock<IRepository<CatalogItem>> _catalogRepositoryMock;

    private CatalogItemUpdateConsumer _catalogItemUpdatedConsumer;

    [TestInitialize]
    public void TestInitialize()
    {
        _catalogRepositoryMock = new Mock<IRepository<CatalogItem>>();
        _catalogItemUpdatedConsumer = new CatalogItemUpdateConsumer(_catalogRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Consume_UpdatesItem()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        string expectedName = "expected name";
        string expectedDescription = "expected description";

        _catalogRepositoryMock.Setup(m => m.GetAsync(expectedId)).ReturnsAsync(new CatalogItem { Id = expectedId });

        Mock<ConsumeContext<CatalogItemUpdated>> consumeContextMock = new Mock<ConsumeContext<CatalogItemUpdated>>();
        consumeContextMock.SetupGet(m => m.Message).Returns(new CatalogItemUpdated(expectedId, expectedName, expectedDescription));

        // Act
        await _catalogItemUpdatedConsumer.Consume(consumeContextMock.Object);

        // Assert
        _catalogRepositoryMock.Verify(m => m.UpdateAsync(It.Is<CatalogItem>(
            i => i.Id == expectedId
            && i.Name == expectedName
            && i.Description == expectedDescription)));
    }

    [TestMethod]
    public async Task Consume_CreatesItenIfNotFound()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        string expectedName = "expected name";
        string expectedDescription = "expected description";

        _catalogRepositoryMock.Setup(m => m.GetAsync(expectedId)).ReturnsAsync(null as CatalogItem);

        Mock<ConsumeContext<CatalogItemUpdated>> consumeContextMock = new Mock<ConsumeContext<CatalogItemUpdated>>();
        consumeContextMock.SetupGet(m => m.Message).Returns(new CatalogItemUpdated(expectedId, expectedName, expectedDescription));

        // Act
        await _catalogItemUpdatedConsumer.Consume(consumeContextMock.Object);

        // Assert
        _catalogRepositoryMock.Verify(m => m.CreateAsync(It.Is<CatalogItem>(
            i => i.Id == expectedId
            && i.Name == expectedName
            && i.Description == expectedDescription)));
    }
}