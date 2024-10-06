using DesignPatterns.Structural.Composite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatternsTest.Structural;

[TestClass]
public class CompositeTests
{
    [TestMethod]
    public void AmazonItemBox_Price_ReturnsPrice()
    {
        // Arrange
        var item1 = new AmazonItem(31);
        var item2 = new AmazonItem(7);
        var item3 = new AmazonItem(104);
        var item4 = new AmazonItem(22);
        
        var box1 = new AmazonItemBox(new[] { item1 });
        var box2 = new AmazonItemBox(new[] { item2, item3 });
        var containingBox = new AmazonItemBox(new IAmazonItem[] { box1, box2, item4 });

        decimal expectedPrice = item1.Price + item2.Price + item3.Price + item4.Price;

        // Act
        decimal actualPrice = containingBox.Price;

        // Assert
        Assert.AreEqual(expectedPrice, actualPrice);
    }
}