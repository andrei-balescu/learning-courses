using System;
using DesignPatterns.Behavioral.Visitor.Companies;
using DesignPatterns.Behavioral.Visitor.CompanyServices;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class VisitorTests
{
    private Mock<ILogger> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [TestMethod]
    public void EmailService_SendsEmailToLawCompany()
    {
        // Arrange
        var expectedCompanyEmail = "test@law.com";
        var lawCompany = new LawCompany(string.Empty, expectedCompanyEmail);

        var emailService = new EmailService(_loggerMock.Object);

        // Act
        lawCompany.Accept(emailService);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedCompanyEmail, StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains("law marketing", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void EmailService_SendsEmailToRestaurantCompany()
    {
        // Arrange
        var expectedCompanyEmail = "test@restaurant.com";
        var restaurantCompany = new RestaurantCompany(string.Empty, expectedCompanyEmail);

        var emailService = new EmailService(_loggerMock.Object);

        // Act
        restaurantCompany.Accept(emailService);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedCompanyEmail, StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains("restaurant marketing", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void PdfExportService_SendsEmailToLawCompany()
    {
        // Arrange
        var expectedCompanyName = "Law Company";
        var lawCompany = new LawCompany(expectedCompanyName, string.Empty);

        var pdfExportService = new PdfExportService(_loggerMock.Object);

        // Act
        lawCompany.Accept(pdfExportService);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedCompanyName, StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains("law data", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void PdfExportService_SendsEmailToRestaurantCompany()
    {
        // Arrange
        var expectedCompanyName = "Restaurant Company";
        var restaurantCompany = new RestaurantCompany(expectedCompanyName, string.Empty);

        var pdfExportService = new PdfExportService(_loggerMock.Object);

        // Act
        restaurantCompany.Accept(pdfExportService);

        // Assert
        _loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedCompanyName, StringComparison.InvariantCultureIgnoreCase)
                && o.ToString().Contains("restaurant data", StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}