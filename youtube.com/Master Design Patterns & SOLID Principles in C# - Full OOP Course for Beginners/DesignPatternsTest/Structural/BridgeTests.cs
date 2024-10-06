using System;
using DesignPatterns.Structural.Bridge;
using DesignPatterns.Structural.Bridge.Brands;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Structural;

[TestClass]
public class BridgeTests
{
    [TestMethod]
    public void RemoteControl_TurnOn_TurnsDeviceOn()
    {
        // Arrange
        var deviceMock = new Mock<IRemoteControlledDeviceDevice>();
        var remoteControl = new RemoteControl(deviceMock.Object);

        // Act
        remoteControl.TurnOn();

        // Assert
        deviceMock.Verify(m => m.TurnOn(), Times.Once);
    }

    [TestMethod]
    public void RemoteControl_TurnOff_TurnsDeviceOff()
    {
        // Arrange
        var deviceMock = new Mock<IRemoteControlledDeviceDevice>();
        var remoteControl = new RemoteControl(deviceMock.Object);

        // Act
        remoteControl.TurnOff();

        // Assert
        deviceMock.Verify(m => m.TurnOff(), Times.Once);
    }

    [TestMethod]
    public void AdvancedRemoteControl_SetChannel_SetsDeviceChannel()
    {
        // Arrange
        int expectedChannel = 543;

        var deviceMock = new Mock<IRemoteControlledDeviceDevice>();
        var remoteControl = new AdvancedRemoteControl(deviceMock.Object);

        // Act
        remoteControl.SetChannel(expectedChannel);

        // Assert
        deviceMock.Verify(m => m.SetChannel(expectedChannel), Times.Once);
    }

    [TestMethod]
    public void SonyRadio_LogsActions()
    {
        // Arrange
        int expectedChannel = 403;

        int actualSequence = 0;
        var loggerMock = new Mock<ILogger>();

        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Sony radio on", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1, ++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedChannel.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(2 ,++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Sony radio off", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(3 ,++actualSequence));

        var sonyRadio = new SonyRadio(loggerMock.Object);

        // Act
        sonyRadio.TurnOn();
        sonyRadio.SetChannel(expectedChannel);
        sonyRadio.TurnOff();

        // Assert
        Assert.AreEqual(3, actualSequence);
    }

    [TestMethod]
    public void SamsungRadio_LogsActions()
    {
        // Arrange
        int expectedChannel = 403;

        int actualSequence = 0;
        var loggerMock = new Mock<ILogger>();

        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Samsung radio on", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(1, ++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedChannel.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(2 ,++actualSequence));
        loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Samsung radio off", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(3 ,++actualSequence));

        var samsungRadio = new SamsungRadio(loggerMock.Object);

        // Act
        samsungRadio.TurnOn();
        samsungRadio.SetChannel(expectedChannel);
        samsungRadio.TurnOff();

        // Assert
        Assert.AreEqual(3, actualSequence);
    }
}