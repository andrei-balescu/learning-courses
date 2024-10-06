using System;
using DesignPatterns.Structural.Proxy;
using DesignPatterns.Structural.Proxy.YouTube;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Structural;

[TestClass]
public class ProxyTest
{
    [TestMethod]
    public void VideoList_Watch_RendersVideo()
    {
        // Arrange
        string videoId = "abcde";

        var videoMock = new Mock<IYoutubeVideo>();
        videoMock.SetupGet(m => m.VideoId).Returns(videoId);
        var videoList = new VideoList();

        // Act
        videoList.Add(videoMock.Object);
        videoList.Watch(videoId);

        // Assert
        videoMock.Verify(m => m.Render(), Times.Once);
    }

    [TestMethod]
    public void YoutubeVideoProxy_DoesNotDownloadVideoUponCreation()
    {
        // Arrange
        var loggerMock = new Mock<ILogger>();
        
        // Act
        var videoProxy = new YoutubeVideoProxy(loggerMock.Object, "abcde");

        // Assert
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Never);
    }

    [TestMethod]
    public void YoutubeVideoProxy_DownloadsVideoUponRendering()
    {
        // Arrange
        string expectedVideoId = "abCde";

        var loggerMock = new Mock<ILogger>();
        int actualSequence = 0;
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Downloading", StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedVideoId, StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1 ,++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Rendering", StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains(expectedVideoId, StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(2 ,++actualSequence));

        var videoProxy = new YoutubeVideoProxy(loggerMock.Object, expectedVideoId);

        // Act
        videoProxy.Render();

        // Assert
        Assert.AreEqual(2, actualSequence);
    }
}