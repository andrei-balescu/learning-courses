using System;
using DesignPatterns.Behavioral.Command;
using DesignPatterns.Behavioral.Command.HtmlCommand;
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
        var remoteCommandMock = new Mock<ICommand>();
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

    // ---------------------------
    // IUndoableCommand tests
    // ---------------------------

    [TestMethod]
    public void EmphasizeCommand_MakesHtmlBold()
    {
        // Arrange
        var htmlDocument = new HtmlDocument("This is a test");
        var emphasizeCommand = new EmphasizeCommand(htmlDocument);
        
        // Act
        emphasizeCommand.Execute();

        // Assert
        Assert.IsTrue(htmlDocument.Content.Contains("<em>", StringComparison.InvariantCultureIgnoreCase));
        Assert.IsTrue(htmlDocument.Content.Contains("</em>", StringComparison.InvariantCultureIgnoreCase));
    }

    [TestMethod]
    public void MakeStrongCommand_MakesHtmlBold()
    {
        // Arrange
        var htmlDocument = new HtmlDocument("This is a test");
        var makeStrongCommand = new MakeStrongCommand(htmlDocument);
        
        // Act
        makeStrongCommand.Execute();

        // Assert
        Assert.IsTrue(htmlDocument.Content.Contains("<strong>", StringComparison.InvariantCultureIgnoreCase));
        Assert.IsTrue(htmlDocument.Content.Contains("</strong>", StringComparison.InvariantCultureIgnoreCase));
    }

    [TestMethod]
    public void HtmlUndoCommand_RestoresContent()
    {
        // Arrange
        string expectedContent = "This is a test";
        var htmlDocument = new HtmlDocument(expectedContent);
        var emphasizeCommand = new EmphasizeCommand(htmlDocument);
        
        // Act
        emphasizeCommand.Execute();
        emphasizeCommand.Unexecute();

        // Assert
        Assert.AreEqual(expectedContent, htmlDocument.Content);
    }

    [TestMethod]
    public void CommandHistory_Execute_UndoesAllCommands()
    {
        // Arrange
        var commandMock = new Mock<IUndoableCommand>();
        var commandHistory = new CommandHistory();

        commandHistory.Execute(commandMock.Object);
        commandHistory.Execute(commandMock.Object);
        
        // Act
        commandHistory.UndoAll();

        // Assert
        commandMock.Verify(m => m.Execute(), Times.Exactly(2));
        commandMock.Verify(m => m.Unexecute(), Times.Exactly(2));
    }
}