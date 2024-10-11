using System;
using DesignPatterns.Creational.Prototype;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Creational;

[TestClass]
public class PrototypeTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void Circle_Draw_LogsAction()
    {
        // Arrange
        int expectedRadius = 12;
        var circle = new Circle(_loggerMock.Object) { Radius = expectedRadius };

        // Act
        circle.Draw();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedRadius.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void Circle_Duplicate_DuplicatesProperties()
    {
        // Arrange
        int expectedRadius = 12;
        var circle = new Circle(_loggerMock.Object) { Radius = expectedRadius };

        // Act
        Circle duplicatedCircle = (Circle)circle.Duplicate();

        // Assert
        Assert.AreEqual(expectedRadius, duplicatedCircle.Radius);
    }

    [TestMethod]
    public void Rectangle_Draw_LogsAction()
    {
        // Arrange
        int expectedWidth = 12;
        int expectedHeight = 23;
        var rectangle = new Rectangle(_loggerMock.Object) { Width = expectedWidth, Height = expectedHeight };

        // Act
        rectangle.Draw();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedWidth.ToString(), StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedHeight.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void Rectangle_Duplicate_DuplicatesProperties()
    {
        // Arrange
        int expectedWidth = 12;
        int expectedHeight = 23;
        var rectangle = new Rectangle(_loggerMock.Object) { Width = expectedWidth, Height = expectedHeight };

        // Act
        Rectangle duplicatedRectangle = (Rectangle)rectangle.Duplicate();

        // Assert
        Assert.AreEqual(expectedWidth, duplicatedRectangle.Width);
        Assert.AreEqual(expectedHeight, duplicatedRectangle.Height);
    }

    [TestMethod]
    public void ShapeActions_Duplicate_CallsDuplicateOnShape()
    {
        // Arrange
        var expectedShape = new Circle(_loggerMock.Object);

        var shapeMock = new Mock<IShape>();
        shapeMock.Setup(m => m.Duplicate()).Returns(expectedShape);

        var shapeActions = new ShapeActions();

        // Act
        var actualShape = shapeActions.Duplicate(shapeMock.Object);

        // Assert
        Assert.AreSame(expectedShape, actualShape);
    }
}