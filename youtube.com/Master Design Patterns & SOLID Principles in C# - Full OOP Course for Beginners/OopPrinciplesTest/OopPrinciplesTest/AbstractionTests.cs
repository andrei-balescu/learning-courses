using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OopPrinciples.Abstraction;

namespace OopPrinciplesTest;

[TestClass]
public class AbstractionTests
{
    private Mock<ILogger> _loggerMock;


    private EmailService _emailService;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
        _emailService = new EmailService(_loggerMock.Object);
    }

    /**
     * See following for mocking ILogger: https://stackoverflow.com/posts/58697253/revisions
     */
    [TestMethod]
    public void AbstractionTests_SendEmail_PassesCorrectStages()
    {
        // Arrange
        int callSequence = 0;
        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSC_CONNECTING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++callSequence, 1));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSG_AUTHENTICATING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++callSequence, 2));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSG_SENDING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++callSequence, 3));

        _loggerMock.Setup(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSG_DISCONNECTING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        )).Callback(() => Assert.AreEqual(++callSequence, 4));

        // Act & Assert
        _emailService.SendEmail();
    }
}