using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidPrinciples.OpenClosedPrinciple;

namespace SolidPrinciplesTest;

[TestClass]
public class OCPTests
{
    [TestMethod]
    public void Circle_CalculateArea_PerformsCorrectCalculation()
    {
        // Arrange
        double inputRadius = 10;
        double expectedArea = Math.PI * Math.Pow(inputRadius, 2);

        var circle = new Circle { Radius = inputRadius };

        // Act
        double actualArea = circle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [TestMethod]
    public void Rectangle_CalculateArea_PerformsCorrectCalculation()
    {
        // Arrange
        double length = 100;
        double width = 50;
        double expectedArea = length * width;

        var rectangle = new Rectangle { Length = length, Width = width };

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
}