using System;
using DesignPatterns.Behavioral.TemplateMethod;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class TemplateMethodTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void TeaBrewer_Prepare_PassesCorrectStages()
    {
        // Arrange
        var teaBrewer = new TeaBrewer(_loggerMock.Object);

        int stageSequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Boiling water", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++stageSequence, 1));
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Pouring water", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++stageSequence, 2));
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Brewing tea", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++stageSequence, 3));
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Adding lemon", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++stageSequence, 4));

        // Act
        teaBrewer.Prepare(true);

        // Assert
        Assert.AreEqual(4, stageSequence);
    }

    [TestMethod]
    public void CoffeeBrewer_Prepare_PassesCorrectStages()
    {
        // Arrange
        var coffeeBrewer = new CoffeeBrewer(_loggerMock.Object);

        int stageSequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Brewing coffee", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++stageSequence, 1));
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Adding cream", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++stageSequence, 2));

        // Act
        coffeeBrewer.Prepare(true);

        // Assert
        Assert.AreEqual(2, stageSequence);
    }

    [TestMethod]
    public void CoffeeBrewer_Prepare_DoesNotAddCondimentsByDefault()
    {
        // Arrange
        var coffeeBrewer = new CoffeeBrewer(_loggerMock.Object);

        int stageSequence = 0;
        
        // Act
        coffeeBrewer.Prepare();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Brewing coffee", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Adding cream", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Never);
    }

    [TestMethod]
    public void CamomilleBrewer_Prepare_ExecutesCorrectActions()
    {
        // Arrange
        var camomilleBrewer = new CamomileBrewer(_loggerMock.Object);

        int stageSequence = 0;
        
        // Act
        camomilleBrewer.Prepare(true);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Brewing camomile", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Adding", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Never);
    }
}