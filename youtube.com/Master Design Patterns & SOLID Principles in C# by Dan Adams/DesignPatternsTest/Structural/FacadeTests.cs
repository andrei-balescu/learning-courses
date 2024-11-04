using System;
using System.Linq;
using DesignPatterns.Structural.Facade;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Structural;

[TestClass]
public class FacadeTests
{
    [TestMethod]
    public void OrderService_Order_CallsDependentServices()
    {
        // Arrange
        var expectedOrderRequest = new OrderRequest
        {
            Name = "Firstname Lastname",
            CardNumber = "1234",
            Amount = 20.99m,
            ItemIds = new []{ "123", "423", "555", "989" }
        };

        var loggerMock = new Mock<ILogger>();
        int actualSequence = 0;
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains($"Authenticating {expectedOrderRequest.Name}", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1 ,++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedOrderRequest.CardNumber, StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedOrderRequest.Amount.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(2 ,++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Inserting items", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(3 ,++actualSequence));

        var orderService = new OrderService(loggerMock.Object);

        // Act
        orderService.Order(expectedOrderRequest);

        // Assert
        Assert.AreEqual(3, actualSequence);

        int expectedNumberOfItems = expectedOrderRequest.ItemIds.Count();
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Checking inventory", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Exactly(expectedNumberOfItems));
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Reducing inventory", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Exactly(expectedNumberOfItems));
    }
}