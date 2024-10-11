using System;
using DesignPatterns.Structural.Decorator;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Structural;

[TestClass]
public class DecoratorTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void CloudData_Save_SavesData()
    {
        // Arrange
        var expectedData = "Sample data";
        var expectedUrl = "cloud.test.com";
        var cloudData = new CloudData(_loggerMock.Object, expectedUrl);

        // Act
        cloudData.Save(expectedData);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedData, StringComparison.InvariantCulture)
                && o.ToString().Contains(expectedUrl, StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void CompressionDecorator_Save_CompressesAndSavesData()
    {
        // Arrange
        var expectedData = "Sample data";
        var storageMock = new Mock<IDataStorage>();

        int actualSequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedData, StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1, ++actualSequence));
        storageMock.Setup(m => m.Save(expectedData))
            .Callback(() => Assert.AreEqual(2, ++actualSequence));

        var compressionDecorator = new CompressionDecorator(_loggerMock.Object, storageMock.Object);

        // Act
        compressionDecorator.Save(expectedData);

        // Assert
        Assert.AreEqual(2, actualSequence);
    }

    [TestMethod]
    public void EncryptionDecorator_Save_EncryptsAndSavesData()
    {
        // Arrange
        var expectedData = "Sample data";
        var storageMock = new Mock<IDataStorage>();

        int actualSequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedData, StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1, ++actualSequence));
        storageMock.Setup(m => m.Save(expectedData))
            .Callback(() => Assert.AreEqual(2, ++actualSequence));

        var encryptionDecorator = new EncryptionDecorator(_loggerMock.Object, storageMock.Object);

        // Act
        encryptionDecorator.Save(expectedData);

        // Assert
        Assert.AreEqual(2, actualSequence);
    }
}
