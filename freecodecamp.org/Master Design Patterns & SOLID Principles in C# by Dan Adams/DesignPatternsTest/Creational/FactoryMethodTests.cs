using System;
using DesignPatterns.Creational.FactoryMethod;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Creational;

[TestClass]
public class FactoryMethodTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void Controller_Render_CreatesBladeViewEngine()
    {
        // Arrange
        string expectedFileName = "/path/to/view";
        var controller = new Controller(_loggerMock.Object);

        // Act
        controller.Render(expectedFileName, null);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Blade", StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedFileName, StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void TwigController_Render_CreatesTwigViewEngine()
    {
        // Arrange
        string expectedFileName = "/path/to/view";
        var controller = new TwigController(_loggerMock.Object);

        // Act
        controller.Render(expectedFileName, null);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Twig", StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedFileName, StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}