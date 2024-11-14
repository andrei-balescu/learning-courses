using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Controllers;
using Playstore.Client.Models;
using Playstore.Client.Models.Auth;
using Playstore.Client.ServiceClients;
using Playstore.Client.Services;

namespace Playstore.Client.Test.Controllers;

[TestClass]
public class AuthControllerTests
{
    private Mock<IAuthClient> _authClientMock;

    private Mock<IJwtTokenService> _jwtTokenServiceMock;

    private Mock<ITokenStorageService> _tokenStorageService;

    private AuthController _authController;

    [TestInitialize]
    public void TestInitialize()
    {
        _authClientMock = new Mock<IAuthClient>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _tokenStorageService = new Mock<ITokenStorageService>();

        _authController = new AuthController(_authClientMock.Object, _jwtTokenServiceMock.Object, _tokenStorageService.Object);

        var tempDataProviderMock = new Mock<ITempDataProvider>();
        var tempDataDictionary = new TempDataDictionary(new DefaultHttpContext(), tempDataProviderMock.Object);
        _authController.TempData = tempDataDictionary;
    }

    [TestMethod]
    public void Login_ReturnsView()
    {
        // Act
        IActionResult result = _authController.Login();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod] 
    public async Task LoginAsync_LogsInUser()
    {
        // Arrange
        var expectedLogin = new LoginViewModel
        {
            UserName = "test user",
            Password = "test password"
        };
        string token = "login token";

        _authClientMock.Setup(m => m.LoginUserAsync(It.Is<LoginRequestDto>(
            r => r.Name == expectedLogin.UserName
            && r.Password == expectedLogin.Password
        ))).ReturnsAsync(new LoginResponseDto(null, token));

        _jwtTokenServiceMock.Setup(m => m.GetPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, token))
            .Returns(new ClaimsPrincipal());

        SetupHttpContextAuthServices();

        // Act
        IActionResult result = await _authController.LoginAsync(expectedLogin);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;

        Assert.AreEqual("Home", redirectResult.ControllerName);
        Assert.AreEqual("Index", redirectResult.ActionName);
    }

    [TestMethod] 
    public async Task LoginAsync_ReturnsErrorIfLoginFailed()
    {
        // Arrange
        var expectedModel = new LoginViewModel();

        _authClientMock.Setup(m => m.LoginUserAsync(It.IsAny<LoginRequestDto>())).ReturnsAsync(null as LoginResponseDto);

        // Act
        IActionResult result = await _authController.LoginAsync(expectedModel);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;

        Assert.AreSame(expectedModel, viewResult.Model);
        Assert.AreEqual(1, _authController.ModelState.ErrorCount);
    }

    [TestMethod]
    public async Task LoginAsync_ReturnsViewIfValidationErrors()
    {
        // Arrange
        _authController.ModelState.AddModelError("test", "test error");

        // Act
        IActionResult result = await _authController.LoginAsync(new LoginViewModel());

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Register_ReturnsView()
    {
        // Act
        IActionResult result = _authController.Register();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;

        Assert.AreEqual(2, _authController.ViewBag.RoleList.Count);
    }

    [TestMethod]
    public async Task RegisterAsync_RegistersUser()
    {
        // Arrange
        var registerDetails = new RegisterViewModel
        {
            UserName = "user name",
            Password = "user password",
            Role = UserRole.Player
        };

        _authClientMock.Setup(m => m.RegisterUserAsync(It.Is<RegisterRequestDto>(
            r => r.Name == registerDetails.UserName
            && r.Password == registerDetails.Password
            && r.role == registerDetails.Role
        ))).ReturnsAsync(new RegisterResponseDto 
        { 
            IsSuccess = true,  
            User = new UserDto(Guid.NewGuid(), "user name")
        });

        // Act
        IActionResult result = await _authController.RegisterAsync(registerDetails);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;

        Assert.AreEqual("Login", redirectResult.ActionName);

        Assert.IsTrue(_authController.TempData.ContainsKey(NotificationsViewModel.c_Success));
    }

    [TestMethod]
    public async Task RegisterAsync_ReturnsBadRequestErrors()
    {
        // Arrange
        _authClientMock.Setup(m => m.RegisterUserAsync(It.IsAny<RegisterRequestDto>()))
            .ReturnsAsync(new RegisterResponseDto 
        { 
            IsSuccess = false,  
            BadRequest = new BadRequestDto
            {
                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "testErrorCode", new List<string> { "test error" } }
                }
            }
        });

        // Act
        IActionResult result = await _authController.RegisterAsync(new RegisterViewModel());

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(1, _authController.ModelState.ErrorCount);
    }

    [TestMethod]
    public async Task LogoutAsync_LogsOutUser()
    {
        // Arrange
        SetupHttpContextAuthServices();

        // Act
        IActionResult result = await _authController.LogoutAsync();

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectResult = (RedirectToActionResult)result;

        Assert.AreEqual("Index", redirectResult.ActionName);
        Assert.AreEqual("Home", redirectResult.ControllerName);
    }

    /// <summary>Sets up dependencies for HttpContext.SignInAsync() / HttpContext.SignOutAsync()</summary>
    private void SetupHttpContextAuthServices()
    {
        // see doc: https://stackoverflow.com/questions/47198341/how-to-unit-test-httpcontext-signinasync
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(m => m.GetService(typeof(IAuthenticationService))).Returns(new Mock<IAuthenticationService>().Object);
        // providing an IServiceProvider to controller might require registration of additional unrelated services (not recommended) 
        serviceProviderMock.Setup(m => m.GetService(typeof(IUrlHelperFactory))).Returns(new Mock<IUrlHelperFactory>().Object);

        _authController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                RequestServices = serviceProviderMock.Object
            }
        };
    }
}