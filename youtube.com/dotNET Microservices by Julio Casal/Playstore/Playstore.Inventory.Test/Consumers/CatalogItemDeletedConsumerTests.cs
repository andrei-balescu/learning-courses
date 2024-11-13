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
public class CatalogItemDeletedConsumerTests
{
    private Mock<IRepository<CatalogItem>> _catalogRepositoryMock;

    private CatalogItemDeletedConsumer _catalogItemDeletedConsumer;

    [TestInitialize]
    public void TestInitialize()
    {
        _catalogRepositoryMock = new Mock<IRepository<CatalogItem>>();
        _catalogItemDeletedConsumer = new CatalogItemDeletedConsumer(_catalogRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Consume_DeletesItem()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();

        _catalogRepositoryMock.Setup(m => m.GetAsync(expectedId)).ReturnsAsync(new CatalogItem { Id = expectedId });

        Mock<ConsumeContext<CatalogItemDeleted>> consumeContextMock = new Mock<ConsumeContext<CatalogItemDeleted>>();
        consumeContextMock.SetupGet(m => m.Message).Returns(new CatalogItemDeleted(expectedId));

        // Act
        await _catalogItemDeletedConsumer.Consume(consumeContextMock.Object);

        // Assert
        _catalogRepositoryMock.Verify(m => m.RemoveAsync(It.Is<Guid>(id => id == expectedId)));
    }
}