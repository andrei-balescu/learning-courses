using System;
using DesignPatterns.Behavioral.Command;
using DesignPatterns.Behavioral.Command.RemoteCommand;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class CommandTests
{
    private Mock<ILogger> _loggerMock;
    private Light _light;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
        _light = new Light(_loggerMock.Object);
    }

    [TestMethod]
    public void RemoteControl_PressButton_ExecutesCommand()
    {
        // Arrange
        var remoteCommandMock = new Mock<IRemoteCommand>();
        var remoteControl = new RemoteControl(remoteCommandMock.Object);

        // Act
        remoteControl.PressButton();

        remoteCommandMock.Verify(m => m.Execute(), Times.Once);
    }

    [TestMethod]
    public void TurnLightOnCommand_Execute_TurnsLightOn()
    {
        // Arrange
        var turnLightOnCommand = new TurnLightOnCommand(_light);

        // Act
        turnLightOnCommand.Execute();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("light is on", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void TurnLightoffCommand_Execute_TurnsLightOn()
    {
        // Arrange
        var turnLightOffCommand = new TurnLightOffCommand(_light);

        // Act
        turnLightOffCommand.Execute();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("light is off", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void DimLightCommand_Execute_TurnsLightOn()
    {
        // Arrange
        var dimLightCommand = new DimLightCommand(_light);

        // Act
        dimLightCommand.Execute();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("light is dim", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}