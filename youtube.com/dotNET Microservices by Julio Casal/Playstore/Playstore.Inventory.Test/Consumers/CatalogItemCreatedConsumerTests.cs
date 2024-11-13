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
public class CatalogItemCreatedConsumerTests
{
    private Mock<IRepository<CatalogItem>> _catalogRepositoryMock;

    private CatalogItemCreatedConsumer _catalogItemCreatedConsumer;

    [TestInitialize]
    public void TestInitialize()
    {
        _catalogRepositoryMock = new Mock<IRepository<CatalogItem>>();
        _catalogItemCreatedConsumer = new CatalogItemCreatedConsumer(_catalogRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Consume_AddsItemToDb()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        string expectedName = "expected name";
        string expectedDescription = "expected description";
        
        Mock<ConsumeContext<CatalogItemCreated>> consumeContextMock = new Mock<ConsumeContext<CatalogItemCreated>>();
        consumeContextMock.SetupGet(m => m.Message).Returns(new CatalogItemCreated(expectedId, expectedName, expectedDescription));

        // Act
        await _catalogItemCreatedConsumer.Consume(consumeContextMock.Object);

        // Assert
        _catalogRepositoryMock.Verify(m => m.CreateAsync(It.Is<CatalogItem>(
            i => i.Id == expectedId
            && i.Name == expectedName
            && i.Description == expectedDescription)));
    }
}