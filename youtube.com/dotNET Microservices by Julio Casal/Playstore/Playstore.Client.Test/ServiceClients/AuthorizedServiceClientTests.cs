using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Client.Services;

namespace Playstore.Client.Test.ServiceClients;

[TestClass]
public class AuthorizedServiceClientTests
{
    [TestMethod]
    public void AuthorizedServiceClien_AddsHeaders()
    {
        // Arrange
        string expectedToken = "jwt token";

        var tokenStorageServiceMock = new Mock<ITokenStorageService>();
        tokenStorageServiceMock.Setup(m => m.Get()).Returns(expectedToken);

        var client = new HttpClient();

        // Act
        var authorizedClient = new TestServiceClient(client, tokenStorageServiceMock.Object);

        // Assert
        KeyValuePair<string, IEnumerable<string>> actualHeader = authorizedClient.DefaultRequestHeaders().Single();
        Assert.AreEqual(actualHeader.Key, "Authorization");

        string actualHeaderValue = actualHeader.Value.Single();
        Assert.AreEqual(actualHeaderValue, $"{$"{JwtBearerDefaults.AuthenticationScheme} {expectedToken}"}");
    }
}