using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OopPrinciples.Coupling;

namespace OopPrinciplesTest;

[TestClass]
public class CouplingTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void EmailSender_SendsMessage()
    {
        // Arrange
        var emailSender = new EmailSender(_loggerMock.Object);
        var order = new Order(emailSender);

        // Act
        order.PlaceOrder();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Order placed successfully", StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains("Sending email", StringComparison.CurrentCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void SmsSender_SendsMessage()
    {
        // Arrange
        var smsSender = new SmsSender(_loggerMock.Object);
        var order = new Order(smsSender);

        // Act
        order.PlaceOrder();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Order placed successfully", StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains("Sending SMS", StringComparison.CurrentCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}