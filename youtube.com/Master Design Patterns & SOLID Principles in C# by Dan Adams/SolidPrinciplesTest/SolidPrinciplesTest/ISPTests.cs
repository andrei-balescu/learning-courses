using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidPrinciples.InterfaceSegregationPrinciple;
using SolidPrinciples.InterfaceSegregationPrinciple.BadExample;

namespace SolidPrinciplesTest;

[TestClass]
public class ISPTests
{
    [TestMethod]
    public void BadCircle_Volume_ThrowsError()
    {
        var circle = new BadCircle { Radius = 10 };

        var isException = false;
        try
        {
            var volume = circle.Volume();
        }
        catch (InvalidOperationException exception)
        {
            isException = exception.Message.Contains("2D", StringComparison.InvariantCultureIgnoreCase);
        }
        finally
        {
            Assert.IsTrue(isException);
        }
    }

    [TestMethod]
    public void Circle_Area_CalculatesCorrectly()
    {
        // Arrange
        var inputRadius = 10;
        var expectedArea = Math.PI * Math.Pow(inputRadius, 2);
        var circle = new Circle { Radius = inputRadius };

        // Act
        var actualArea = circle.Area();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [TestMethod]
    public void Sphere_Area_CalculatesCorrectly()
    {
        // Arrange
        var inputRadius = 10;
        var expectedArea = 4 * Math.PI * Math.Pow(inputRadius, 2);
        var sphere = new Sphere { Radius = inputRadius };

        // Act
        var actualArea = sphere.Area();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [TestMethod]
    public void Sphere_Volume_CalculatesCorrectly()
    {
        // Arrange
        var inputRadius = 10;
        var expectedVolume = (4.0 / 3.0) * Math.PI * Math.Pow(inputRadius, 3);
        var sphere = new Sphere { Radius = inputRadius };

        // Act
        var actualVolume = sphere.Volume();

        // Assert
        Assert.AreEqual(expectedVolume, actualVolume);
    }
}