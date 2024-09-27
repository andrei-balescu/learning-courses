using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolidPrinciples.SingleResponsabilityPrinciple;

namespace SolidPrinciplesTest;

[TestClass]
public class SRPTests
{
    [TestMethod]
    public void EmailSender_SendEmail_LogsAction()
    {
        // Arrange
        var loggerMock = new Mock<ILogger>();
        var emailSender = new EmailSender(loggerMock.Object);

        var expectedEmail = "email@example.com";
        var expectedMessage = "Welcome to our platform.";

        // Act
        emailSender.SendEmail(expectedEmail, expectedMessage);

        // Assert
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedEmail, StringComparison.InvariantCulture)
                && o.ToString().Contains(expectedMessage, StringComparison.CurrentCulture)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);

    }

    [TestMethod]
    public void UserService_Register_SendsEmail()
    {
        // Arrange
        var emailSenderMock = new Mock<IEmailSender>();
        var userService = new UserService(emailSenderMock.Object);

        var expectedEmail = "email@example.com";
        var user = new User { Username = "TestUser", Email = expectedEmail };

        // Act
        userService.Register(user);

        // Assert
        emailSenderMock.Verify(m => m.SendEmail(expectedEmail, It.IsNotNull<string>()), Times.Once);
    }
}