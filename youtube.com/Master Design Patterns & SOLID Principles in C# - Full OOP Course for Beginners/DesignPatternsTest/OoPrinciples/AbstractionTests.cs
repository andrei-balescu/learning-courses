using System;
using System.Runtime.CompilerServices;
using DesignPatternsLibrary.OopPrinciples.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.OopPrinciples;

[TestClass]
public class AbstractionTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Mock<ILogger> _loggerMock;


    private EmailService _emailService;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
        _emailService = new EmailService(_loggerMock.Object);
    }

    [TestMethod]
    public void AbstractionTests_SendEmail_PassesCorrectStages()
    {
        // Arrange
        int sequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSC_CONNECTING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++sequence, 1));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSG_AUTHENTICATING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++sequence, 2));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSG_SENDING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++sequence, 3));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSG_DISCONNECTING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++sequence, 4));

        // Act & Assert
        _emailService.SendEmail();
    }
}