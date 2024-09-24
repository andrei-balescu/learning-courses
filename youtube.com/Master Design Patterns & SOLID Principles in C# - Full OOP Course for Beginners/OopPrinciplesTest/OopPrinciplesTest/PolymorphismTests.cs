using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OopPrinciples.Polymorphism;

namespace OopPrinciplesTest;

[TestClass]
public class PolymorphismTests
{
    [TestMethod]
    public void VehicleInspection_StartsAndStopsAllVehicles()
    {
        // Arrange
        Mock<ILogger<Car>> carLoggerMock = new Mock<ILogger<Car>>();
        Car car = new Car(carLoggerMock.Object);

        Mock<ILogger<Motorcycle>> motorcycleLoggerMock = new Mock<ILogger<Motorcycle>>();
        Motorcycle motorcycle = new Motorcycle(motorcycleLoggerMock.Object);

        IList<Vehicle> vehicles = new List<Vehicle>() { car, motorcycle };
        VehicleInspection inspector = new VehicleInspection();

        // Act
        inspector.InspectVehicles(vehicles);

        // Assert
        carLoggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Car is starting", System.StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
        carLoggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Car is stopping", System.StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
        motorcycleLoggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Motorcycle is starting", System.StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
        motorcycleLoggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Motorcycle is stopping", System.StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}