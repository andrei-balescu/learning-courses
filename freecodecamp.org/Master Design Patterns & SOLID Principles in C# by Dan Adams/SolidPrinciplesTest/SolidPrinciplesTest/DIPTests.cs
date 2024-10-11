using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolidPrinciples.DependencyInversionPrinciple;

namespace SolidPrinciplesTest;

[TestClass]
public class DIPTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void Engine_Start_LogsAction()
    {
        // Arrange
        var engine = new Engine(_loggerMock.Object);

        // Act
        engine.Start();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("engine started", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void Car_Start_StartsEngine()
    {
        // Arrange
        var engineMock = new Mock<IEngine>();

        var car = new Car(engineMock.Object, _loggerMock.Object);

        // Act
        car.StartCar();

        // Assert
        engineMock.Verify(m => m.Start(), Times.Once);

        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("car started", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}