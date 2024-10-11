using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidPrinciples.LiskovSubstitutionPrinciple;

namespace SolidPrinciplesTest;

[TestClass]
public class LSPTests
{
    [TestMethod]
    public void Rectangle_Area_CalculatesCorrectly()
    {
        // Arrange
        var inputWidth = 10;
        var inputHeight = 5;

        var expectedArea = inputWidth * inputHeight;

        Shape rectangle = new Rectangle { Width = inputWidth, Height = inputHeight };

        // Act & Assert
        Assert.AreEqual(expectedArea, rectangle.Area);
    }

    /**
     * Test similar to Rectangle test which BadSquare inherits.
     */
    [TestMethod]
    public void BadSquare_Area_GivesBadResult()
    {
        // Arrange
        var inputWidth = 10;
        var inputHeight = 5;

        var expectedArea = inputWidth * inputHeight;

        Rectangle square = new BadSquare { Width = inputWidth, Height = inputHeight };

        // Act & Assert
        Assert.AreNotEqual(expectedArea, square.Area);
    }

    [TestMethod]
    public void Square_Area_CalculatesCorrectly()
    {
        // Arrange
        var inputSideLength = 5;

        var expectedArea = inputSideLength * inputSideLength;

        Shape rectangle = new Square { SideLength = inputSideLength };

        // Act & Assert
        Assert.AreEqual(expectedArea, rectangle.Area);
    }
}