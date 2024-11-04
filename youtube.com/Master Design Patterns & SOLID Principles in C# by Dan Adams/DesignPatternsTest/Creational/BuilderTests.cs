using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Builder.Build;
using DesignPatterns.Creational.Builder.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatternsTest.Creational;

[TestClass]
public class BuilderTests
{
    [TestMethod]
    public void ManualBuilder_BuildsAllComponents()
    {
        // Arrange
        var expectedCarType = CarType.Saloon;
        int expectedSeats = 3;
        string expectedIsConvertible = "Convertible";
        int expectedWheelsDiameter = 21;
        string expectedGpsInfo = "Info on GPS";

        var carManualBuilder = new CarManualBuilder
        {
            CarType = expectedCarType,
            IsConvertible = true,
            Seats = expectedSeats,
            Engine = new Engine(),
            Dashboard = new Dashboard(hasRevCounter: false),
            Wheels = new Wheels(diameterInInches: expectedWheelsDiameter),
            GPSNavigator = new GPSNavigator()
        };

        // Act
        CarManual manual = carManualBuilder.GetManual();

        // Assert
        string manualText = manual.Print();
        Assert.IsTrue(manualText.Contains(expectedCarType.ToString()));
        Assert.IsTrue(manualText.Contains($"Seats: {expectedSeats}"));
        Assert.IsTrue(manualText.Contains(expectedIsConvertible));
        Assert.IsTrue(manualText.Contains($"diameter in inches = {expectedWheelsDiameter}"));
        Assert.IsTrue(manualText.Contains(expectedGpsInfo));
    }

    [TestMethod]
    public void ManualBuilder_OmitsMissingComponents()
    {
        // Arrange
        string notExpectedIsConvertible = "Convertible";
        string notExpectedGpsInfo = "Info on GPS";

        var carManualBuilder = new CarManualBuilder
        {
            IsConvertible = false,
            Engine = new Engine(),
            Dashboard = new Dashboard(hasRevCounter: false),
            Wheels = new Wheels(diameterInInches: 10),
            GPSNavigator = null
        };

        // Act
        CarManual manual = carManualBuilder.GetManual();

        // Assert
        string manualText = manual.Print();
        Assert.IsFalse(manualText.Contains(notExpectedIsConvertible));
        Assert.IsFalse(manualText.Contains(notExpectedGpsInfo));
    }

    [TestMethod]
    public void BuildDirector_ConstructSportsCar_BuildsCorrectConfiguration()
    {
        // Arrange
        var expectedCarType = CarType.Sports;
        int expectedSeats = 2;
        string expectedIsConvertible = "Convertible";
        int expectedWheelsDiameter = 20;
        string notExpectedxpectedGpsInfo = "Info on GPS";

        var carManualBuilder = new CarManualBuilder();
        var buildDirector = new BuildDirector();

        // Act
        buildDirector.ConstructSportsCar(carManualBuilder);
        CarManual manual = carManualBuilder.GetManual();

        // Assert
        string manualText = manual.Print();
        Assert.IsTrue(manualText.Contains(expectedCarType.ToString()));
        Assert.IsTrue(manualText.Contains($"Seats: {expectedSeats}"));
        Assert.IsTrue(manualText.Contains(expectedIsConvertible));
        Assert.IsTrue(manualText.Contains($"diameter in inches = {expectedWheelsDiameter}"));
        Assert.IsFalse(manualText.Contains(notExpectedxpectedGpsInfo));
    }

    [TestMethod]
    public void BuildDirector_ConstructSUV_BuildsCorrectConfiguration()
    {
        // Arrange
        var expectedCarType = CarType.SUV;
        int expectedSeats = 5;
        string notExpectedIsConvertible = "Convertible";
        int expectedWheelsDiameter = 19;
        string expectedxpectedGpsInfo = "Info on GPS";

        var carManualBuilder = new CarManualBuilder();
        var buildDirector = new BuildDirector();

        // Act
        buildDirector.ConstructSUV(carManualBuilder);
        CarManual manual = carManualBuilder.GetManual();

        // Assert
        string manualText = manual.Print();
        Assert.IsTrue(manualText.Contains(expectedCarType.ToString()));
        Assert.IsTrue(manualText.Contains($"Seats: {expectedSeats}"));
        Assert.IsFalse(manualText.Contains(notExpectedIsConvertible));
        Assert.IsTrue(manualText.Contains($"diameter in inches = {expectedWheelsDiameter}"));
        Assert.IsTrue(manualText.Contains(expectedxpectedGpsInfo));
    }
}