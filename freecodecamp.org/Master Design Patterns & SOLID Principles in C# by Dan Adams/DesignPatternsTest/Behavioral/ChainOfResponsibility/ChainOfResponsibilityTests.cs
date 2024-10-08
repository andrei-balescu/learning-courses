using System;
using DesignPatterns.Behavioral.ChainOfResponsibility;
using DesignPatterns.Behavioral.ChainOfResponsibility.RequestHandlers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral.ChainOfResponsibility;

[TestClass]
public class ChainOfResponsibilityTests
{
    [TestMethod]
    public void RequestValidator_Handle_TrimsText()
    {
        // Arrange
        string expectedUserName = "username";
        string expectedPassword = "password";
        var httpRequest = new HttpRequest($" {expectedUserName} ", $" {expectedPassword} ");

        var nextHandlerMock = new Mock<MockRequestHandler>();
        var requestValidator = new RequestValidator(nextHandlerMock.Object);

        // Act
        requestValidator.Handle(httpRequest);

        // Assert
        Assert.AreEqual(expectedUserName, httpRequest.Username);
        Assert.AreEqual(expectedPassword, httpRequest.Password);
        nextHandlerMock.Verify(m => m.MockHandle(It.Is<HttpRequest>(r => r == httpRequest)), Times.Once);
    }

    [TestMethod]
    public void RequestValidator_Handle_StopsHandlingIfEmptyValues()
    {
        // Arrange
        var httpRequest = new HttpRequest(string.Empty, string.Empty);

        var nextHandlerMock = new Mock<MockRequestHandler>();
        var requestValidator = new RequestValidator(nextHandlerMock.Object);

        // Act
        requestValidator.Handle(httpRequest);

        // Assert
        nextHandlerMock.Verify(m => m.MockHandle(It.IsAny<HttpRequest>()), Times.Never);
    }

    [TestMethod]
    public void RequestAuthenticator_Handle_AuthenticatesUser()
    {
        // Arrange
        string expectedUserName = "GoodUser";
        string expectedPassword = "GoodPassword";
        var httpRequest = new HttpRequest(expectedUserName, expectedPassword);

        var nextHandlerMock = new Mock<MockRequestHandler>();
        var requestAuthenticator = new RequestAuthenticator(nextHandlerMock.Object);

        // Act
        requestAuthenticator.Handle(httpRequest);

        // Assert
        nextHandlerMock.Verify(m => m.MockHandle(It.Is<HttpRequest>(r => r == httpRequest)), Times.Once);
    }

    [TestMethod]
    public void RequestAuthenticator_Handle_StopsHandlingIfNotAuthenticated()
    {
        // Arrange
        var httpRequest = new HttpRequest("BadUser", "BadPassword");

        var nextHandlerMock = new Mock<MockRequestHandler>();
        var requestAuthenticator = new RequestAuthenticator(nextHandlerMock.Object);

        // Act
        requestAuthenticator.Handle(httpRequest);

        // Assert
        nextHandlerMock.Verify(m => m.MockHandle(It.IsAny<HttpRequest>()), Times.Never);
    }

    [TestMethod]
    public void RequestLogger_Handle_LogsRequest()
    {   
        // Arrange
        var httpRequest = new HttpRequest(string.Empty, string.Empty);

        var loggerMock = new Mock<ILogger>();
        var nextHandlerMock = new Mock<MockRequestHandler>();
        var currentHandler = new RequestLogger(loggerMock.Object, nextHandlerMock.Object);
        
        // Act
        currentHandler.Handle(httpRequest);

        // Assert
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains("Request", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);

        nextHandlerMock.Verify(m => m.MockHandle(It.Is<HttpRequest>(r => r == httpRequest)), Times.Once);
    }
    
    [TestMethod]
    public void WebServer_Handle_ExecutesHandler()
    {
        // Arrange
        var httpRequest = new HttpRequest(string.Empty, string.Empty);

        var handlerMock = new Mock<MockRequestHandler>();
        var webServer = new WebServer(handlerMock.Object);

        // Act
        webServer.Handle(httpRequest);

        // Assert
        handlerMock.Verify(m => m.MockHandle(It.Is<HttpRequest>(r => r == httpRequest)), Times.Once);
    }
}