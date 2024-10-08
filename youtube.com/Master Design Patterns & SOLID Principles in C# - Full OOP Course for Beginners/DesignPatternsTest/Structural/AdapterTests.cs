using System;
using DesignPatterns.Structural.Adapter;
using DesignPatterns.Structural.Adapter.ColorFilters;
using DesignPatterns.Structural.Adapter.Lib3rdParty;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Structural;

[TestClass]
public class AdapterTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void VideoEditor_AppllyFilter_AppliesFilter()
    {
        // Arrange
        var filterMock = new Mock<IColorFilter>();
        var expectedVideo = new Video();
        var videoEditor = new VideoEditor(expectedVideo);

        // Act
        videoEditor.ApplyFilter(filterMock.Object);

        // Assert
        filterMock.Verify(m => m.Apply(expectedVideo));
    }

    [TestMethod]
    public void BlackAndWhiteFilter_Apply_LogsAction()
    {
        // Arrange
        var blackAndWhiteFilter = new BlackAndWhiteFilter(_loggerMock.Object);

        // Act
        blackAndWhiteFilter.Apply(new Video());

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
    public void MidnightPurple_Apply_LogsAction()
    {
        // Arrange
        var midnightPurpleFilter = new MidnightPurpleFilter(_loggerMock.Object);

        // Act
        midnightPurpleFilter.Apply(new Video());

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("midnight-purple", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void RainbowFilter_Apply_Applies3rdPartyFilter()
    {
        // Arrange
        float expectedBrightness = 64.23f;
        
        var actualSequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedBrightness.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1, ++actualSequence));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("rainbow color", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(2 ,++actualSequence));

        var rainbow = new Rainbow(_loggerMock.Object);
        rainbow.Setup(expectedBrightness);
        var rainbowFilter = new RainbowFilter(rainbow);

        // Act
        rainbowFilter.Apply(new Video());

        // Assert
        Assert.AreEqual(2, actualSequence);
    }
}