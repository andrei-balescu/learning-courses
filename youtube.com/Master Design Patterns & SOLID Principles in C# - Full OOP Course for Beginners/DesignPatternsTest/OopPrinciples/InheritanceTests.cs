using System;
using DesignPatternsLibrary.OopPrinciples.Inheritance;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.OopPrinciples;

[TestClass]
public class InheritanceTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void Vehicle_Start_LogsAction()
    {
        // Arrange
        Bike bike = new Bike("Schwinn", "Mountainbike", 2022, _loggerMock.Object);

        // Act
        bike.Start();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("starting", System.StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ));

    }

    [TestMethod]
    public void Vehicle_Stop_LogsAction()
    {
        // Arrange
        Bike bike = new Bike("Schwinn", "Mountainbike", 2022, _loggerMock.Object);

        // Act
        bike.Stop();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("stopping", System.StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ));

    }

    [TestMethod]
    public void Car_PassesConstructorArguments()
    {
        // Arrange
        string expectedBrand = "Ford";
        string expectedModel = "Focus";
        int expectedYear = 2022;
        int expectedNumberOfDoors = 4;

        // Act
        Car car = new Car(expectedBrand, expectedModel, expectedYear, expectedNumberOfDoors, _loggerMock.Object);

        // Assert
        Assert.AreEqual(expectedBrand, car.Brand);
        Assert.AreEqual(expectedModel, car.Model);
        Assert.AreEqual(expectedYear, car.Year);
        Assert.AreEqual(expectedNumberOfDoors, car.NumberOfDoors);
    }

    [TestMethod]
    public void Bike_PassesConstructorArguments()
    {
        // Arrange
        string expectedBrand = "Schwinn";
        string expectedModel = "Mountainbike";
        int expectedYear = 2022;

        // Act
        Bike bike = new Bike(expectedBrand, expectedModel, expectedYear, _loggerMock.Object);

        // Assert
        Assert.AreEqual(expectedBrand, bike.Brand);
        Assert.AreEqual(expectedModel, bike.Model);
        Assert.AreEqual(expectedYear, bike.Year);
    }

}