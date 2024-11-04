using DesignPatterns.Structural.Flyweight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Structural;

[TestClass]
public class FlyweightTests
{
    [TestMethod]
    public void CropIconFactory_GetIcon_ReturnsSameObjectForType()
    {
        // Arrange
        var cropIconFactory = new CropIconFactory();

        var expectedCropType = CropType.Carrot;
        CropIcon expectedCropIcon = cropIconFactory.GetCropIcon(expectedCropType);

        var newCropType = CropType.Potato;
        CropIcon newTypeCropIcon = cropIconFactory.GetCropIcon(newCropType);

        // Act
        CropIcon actualCropIcon = cropIconFactory.GetCropIcon(expectedCropType);

        // Assert
        Assert.AreSame(expectedCropIcon, actualCropIcon);
        Assert.AreNotSame(newTypeCropIcon, actualCropIcon);
    }

    [TestMethod]
    public void CropFactory_CreateCrop_GetsIconFromCropIconFactory()
    {
        // Arrange
        var expectedCropType = CropType.Wheat;

        var cropIconFactoryMock = new Mock<ICropIconFactory>();
        var cropFactory = new CropFactory(cropIconFactoryMock.Object);

        // Act
        Crop actualCrop = cropFactory.CreateCrop(23, 43, expectedCropType);

        // Assert
        cropIconFactoryMock.Verify(m => m.GetCropIcon(expectedCropType), Times.Once);
    }
}