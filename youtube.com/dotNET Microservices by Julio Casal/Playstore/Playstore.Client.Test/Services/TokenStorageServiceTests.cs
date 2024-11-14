using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Client.Services;

namespace Playstore.Client.Test.Services;

[TestClass]
public class TokenStorageServiceTests
{
    private TokenStorageService _tokenStorageService;

    private Mock<IResponseCookies> _responseCookiesMock;

    private Mock<IRequestCookieCollection> _requestCookiesMock;

    private const string c_expectedCookieName = "jwtToken";

    [TestInitialize]
    public void TestInitialize()
    {
        _responseCookiesMock = new Mock<IResponseCookies>();
        _requestCookiesMock = new Mock<IRequestCookieCollection>();

        var httpResponseMock = new Mock<HttpResponse>();
        httpResponseMock.SetupGet(m => m.Cookies).Returns(_responseCookiesMock.Object);

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.SetupGet(m => m.Cookies).Returns(_requestCookiesMock.Object);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.SetupGet(m => m.Response).Returns(httpResponseMock.Object);
        httpContextMock.SetupGet(m => m.Request).Returns(httpRequestMock.Object);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.SetupGet(m => m.HttpContext).Returns(httpContextMock.Object);

        _tokenStorageService = new TokenStorageService(httpContextAccessorMock.Object);
    }

    [TestMethod]
    public void Store_StoresToken()
    {
        // Arrange
        string expectedToken = "expected token";

        // Act
        _tokenStorageService.Store(expectedToken);

        // Assert
        _responseCookiesMock.Verify(m => m.Append(
            It.Is<string>(s => s == c_expectedCookieName),
            It.Is<string>(s => s == expectedToken)
        ));
    }

    [TestMethod]
    public void Get_ReturnsValue()
    {
        // Arrange
        string expectedToken = "expected token";

        _requestCookiesMock.Setup(m => m.TryGetValue(c_expectedCookieName, out expectedToken));

        // Act
        var actualToken = _tokenStorageService.Get();

        // Assert
        Assert.AreEqual(expectedToken, actualToken);
    }

    [TestMethod]
    public void Clear_ClearsToken()
    {
        // Act
        _tokenStorageService.Clear();

        // Assert
        _responseCookiesMock.Verify(m => m.Delete(c_expectedCookieName));
    }
}