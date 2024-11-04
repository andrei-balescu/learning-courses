using System;
using DesignPatterns.Creational.AbstractFactory;
using DesignPatterns.Creational.AbstractFactory.UIMac;
using DesignPatterns.Creational.AbstractFactory.UIWindows;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Creational;

[TestClass]
public class AbstractFactoryTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void MacButton_Render_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new MacUIComponentFactory(_loggerMock.Object);
        IButton button = macUIComponentFactory.CreateButton();

        // Act
        button.Render();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Mac: rendering button", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void MacButton_OnClick_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new MacUIComponentFactory(_loggerMock.Object);
        IButton button = macUIComponentFactory.CreateButton();

        // Act
        button.OnClick();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Mac: button has been clicked", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void MacCheckbox_Render_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new MacUIComponentFactory(_loggerMock.Object);
        ICheckbox checkbox = macUIComponentFactory.CreateCheckbox();

        // Act
        checkbox.Render();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Mac: rendering checkbox", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void MacCheckbox_OnSelect_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new MacUIComponentFactory(_loggerMock.Object);
        ICheckbox checkbox = macUIComponentFactory.CreateCheckbox();

        // Act
        checkbox.OnSelect();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Mac: checkbox has been selected", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void WindowsButton_Render_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new WindowsUIComponentFactory(_loggerMock.Object);
        IButton button = macUIComponentFactory.CreateButton();

        // Act
        button.Render();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Windows: rendering button", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void WindowsButton_OnClick_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new WindowsUIComponentFactory(_loggerMock.Object);
        IButton button = macUIComponentFactory.CreateButton();

        // Act
        button.OnClick();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Windows: button has been clicked", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void WindowsCheckbox_Render_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new WindowsUIComponentFactory(_loggerMock.Object);
        ICheckbox checkbox = macUIComponentFactory.CreateCheckbox();

        // Act
        checkbox.Render();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Windows: rendering checkbox", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void WindowsCheckbox_OnSelect_RendersComponent()
    {
        // Arrange
        var macUIComponentFactory = new WindowsUIComponentFactory(_loggerMock.Object);
        ICheckbox checkbox = macUIComponentFactory.CreateCheckbox();

        // Act
        checkbox.OnSelect();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Windows: checkbox has been selected", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void UserSettingsForm_Render_CreatesAndRendersComponents()
    {
        // Arrange
        var buttonMock = new Mock<IButton>();
        var checkboxMock = new Mock<ICheckbox>();
        var uiComponentFactoryMock = new Mock<IUIComponentFactory>();
        uiComponentFactoryMock.Setup(m => m.CreateButton()).Returns(buttonMock.Object);
        uiComponentFactoryMock.Setup(m => m.CreateCheckbox()).Returns(checkboxMock.Object);
        var userSettingsForm = new UserSettingsForm(uiComponentFactoryMock.Object);

        // Act
        userSettingsForm.Render();

        // Assert
        buttonMock.Verify(m => m.Render(), Times.Once);
        checkboxMock.Verify(m => m.Render(), Times.Once);
    }
}