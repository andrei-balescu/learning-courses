using System;
using DesignPatternsLibrary.OopPrinciples.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.OopPrinciples;

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

    [TestMethod]
    public void AbstractionTests_SendEmail_PassesCorrectStages()
    {
        // Arrange & Act
        _emailService.SendEmail();

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals(EmailService.MSC_CONNECTING, o.ToString(), System.StringComparison.InvariantCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ));
    }
}