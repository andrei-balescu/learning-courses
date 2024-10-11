using System;
using DesignPatterns.Behavioral.Strategy;
using DesignPatterns.Behavioral.Strategy.Compressor;
using DesignPatterns.Behavioral.Strategy.Overlay;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class StrategyTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void CompressorMOV_Compress_LogsAction()
    {
        // Arrange
        var compressorMOV = new CompressorMOV(_loggerMock.Object);

        // Act
        compressorMOV.Compress();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("MOV", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void CompressorMOV_FileExtension_ReturnCorrectExtension()
    {
        // Arrange
        var compressorMOV = new CompressorMOV(_loggerMock.Object);

        // Act & Assert
        Assert.AreEqual<string>("mov", compressorMOV.FileExtension.ToLowerInvariant());
    }

    [TestMethod]
    public void CompressorMP4_Compress_LogsAction()
    {
        // Arrange
        var compressorMP4 = new CompressorMP4(_loggerMock.Object);

        // Act
        compressorMP4.Compress();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("MP4", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void CompressorMP4_FileExtension_ReturnCorrectExtension()
    {
        // Arrange
        var compressorMP4 = new CompressorMP4(_loggerMock.Object);

        // Act & Assert
        Assert.AreEqual<string>("mp4", compressorMP4.FileExtension.ToLowerInvariant());
    }

    [TestMethod]
    public void OverlayBlackAndWhite_Apply_LogsAction()
    {
        // Arrange
        var overlayBlackAndWhite = new OverlayBlackAndWhite(_loggerMock.Object);

        // Act
        overlayBlackAndWhite.Apply();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("black and white", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void OverlayBlur_Apply_LogsAction()
    {
        // Arrange
        var overlayBlur = new OverlayBlur(_loggerMock.Object);

        // Act
        overlayBlur.Apply();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("blur", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void VideoStorage_Store_AppliesCompressionAndOverlay()
    {
        // Arrange
        var expectedFileName = "expected/file-name";
        var expectedFileExtension = "avi";

        var compressorMock = new Mock<ICompressor>();
        compressorMock.Setup(m => m.FileExtension).Returns(expectedFileExtension);

        var overlayMock = new Mock<IOverlay>();

        var videoStorage = new VideoStorage(_loggerMock.Object, compressorMock.Object, overlayMock.Object);

        // Act
        videoStorage.Store(expectedFileName);

        // Assert
        compressorMock.Verify(m => m.Compress(), Times.Once);
        overlayMock.Verify(m => m.Apply(), Times.Once);

        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedFileName, StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedFileExtension, StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void VideoStorage_Store_SkipsNullOverlay()
    {
        // Arrange
        var compressorMock = new Mock<ICompressor>();

        var videoStorage = new VideoStorage(_loggerMock.Object, compressorMock.Object);

        // Act
        videoStorage.Store("file-name");

        // Assert
        compressorMock.Verify(m => m.Compress(), Times.Once);
    }
}