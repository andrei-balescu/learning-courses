using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Respositories;
using Playstore.Auth.Service.Controllers;
using Playstore.Auth.Service.Services;

namespace Playstore.Auth.Test.Controllers;

[TestClass]
public class AuthControllerTests
{
    private Mock<IAuthService> _authServiceMock;

    private Mock<IJwtTokenService> _jwtTokenServiceMock;

    private Mock<IUserRepository> _userRepositoryeMock;

    private AuthController _authController;

    [TestInitialize]
    public void TestInitialize()
    {
        _authServiceMock = new Mock<IAuthService>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _userRepositoryeMock = new Mock<IUserRepository>();

        _authController = new AuthController(_authServiceMock.Object, _jwtTokenServiceMock.Object, _userRepositoryeMock.Object);
    }

    [TestMethod]
    public async Task Register_RegistersUser()
    {
        // Arrange
        string expectedName = "test name";
        Guid expectedUserId = Guid.NewGuid();
        RegisterRequestDto registrationRequest = new(expectedName, "test password", UserRole.Player);

        _authServiceMock.Setup(m => m.RegisterUserAsync(registrationRequest)).ReturnsAsync(null as IEnumerable<IdentityError>);
        _userRepositoryeMock.Setup(m => m.GetUser(It.IsAny<Func<IdentityUser, bool>>())).Returns((Func<IdentityUser, bool> predicate) =>
        {
            var userList = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = expectedUserId.ToString(),
                    UserName = expectedName
                }
            };
            IdentityUser user = userList.SingleOrDefault(predicate);
            return user;
        });

        // Act
        IActionResult result = await _authController.RegisterAsync(registrationRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        var actualUser = (UserDto)okResult.Value;

        Assert.AreEqual(expectedName, actualUser.Name);
        Assert.AreEqual(expectedUserId, actualUser.Id);
    }

    [TestMethod]
    public async Task Register_ReturnsValidationErrors()
    {
        // Arrange
        string expectedErrorCode = "errorCode";
        string expectedErrorMessage = "error message";
        string expectedTraceIdentifier = "trace identifier";

        RegisterRequestDto registrationRequest = new("test name", "test password", UserRole.Player);


        _authServiceMock.Setup(m => m.RegisterUserAsync(registrationRequest))
            .ReturnsAsync(new List<IdentityError> { new IdentityError { Code = expectedErrorCode, Description = expectedErrorMessage } });

        Mock<HttpContext> httpContextMock = new();
        httpContextMock.SetupGet(m => m.TraceIdentifier).Returns(expectedTraceIdentifier);
        _authController.ControllerContext.HttpContext = httpContextMock.Object;

        // Act
        IActionResult result = await _authController.RegisterAsync(registrationRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = (BadRequestObjectResult)result;
        var validationProblems = (ValidationProblemDetails)badRequestResult.Value;

        Assert.AreEqual(expectedTraceIdentifier, validationProblems.Extensions["traceId"]);
        string actualErrorMessage = validationProblems.Errors[expectedErrorCode].Single();
        Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
    }

    [TestMethod]
    public async Task Login_LogsInUser()
    {
        // Arrange
        Guid expectedUserId = Guid.NewGuid();
        string expectedUserName = "loginName";
        string expectedToken = "expected token";

        LoginRequestDto loginRequestDto = new("loginName", "Password");
        IdentityUser user = new IdentityUser
        {
            Id = expectedUserId.ToString(),
            UserName = expectedUserName
        };

        _authServiceMock.Setup(m => m.LoginUserAsync(loginRequestDto)).ReturnsAsync(user);
        _jwtTokenServiceMock.Setup(m => m.GenerateTokenAsync(user)).ReturnsAsync(expectedToken);

        // Act
        var result = await _authController.LoginAsync(loginRequestDto);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        var actualResponse = (LoginResponseDto)okResult.Value;
        Assert.AreEqual(expectedUserId, actualResponse.User.Id);
        Assert.AreEqual(expectedUserName, actualResponse.User.Name);
        Assert.AreEqual(expectedToken, actualResponse.Token);
    }

    [TestMethod]
    public async Task Register_BadResponseIfLoginFails()
    {
        // Arrange
        LoginRequestDto loginRequestDto = new("loginName", "Password");

        _authServiceMock.Setup(m => m.LoginUserAsync(loginRequestDto)).ReturnsAsync(null as IdentityUser);

        // Act
        var result = await _authController.LoginAsync(loginRequestDto);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }
}