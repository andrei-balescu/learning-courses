using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Respositories;
using Playstore.Auth.Service.Services;

namespace Playstore.Auth.Test.Services;

[TestClass]
/// <summary>See doc: https://code-maze.com/aspnetcore-identity-testing-usermanager-rolemanager/</summary>
public class AuthServiceTests
{
    private Mock<UserManager<IdentityUser>> _userManagerMock;

    private Mock<RoleManager<IdentityRole>> _roleManagerMock;

    private Mock<IUserRepository> _userRepositoryMock;

    private AuthService _authService;

    [TestInitialize]
    public void TestInitialize()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<IdentityUser>>().Object,
            new IUserValidator<IdentityUser>[0],
            new IPasswordValidator<IdentityUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<IdentityUser>>>().Object
        );

        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
            new Mock<IRoleStore<IdentityRole>>().Object,
            new IRoleValidator<IdentityRole>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<IdentityRole>>>().Object
        );

        _userRepositoryMock = new Mock<IUserRepository>();

        _authService = new AuthService(_userManagerMock.Object, _roleManagerMock.Object, _userRepositoryMock.Object);
    }

    [TestMethod]
    public async Task LoginUserAsync_RegistersUser()
    {
        // Arrange
        string loginName = "login name";
        string loginPassword = "password";
        LoginRequestDto loginRequest = new(loginName, loginPassword);
        IdentityUser expectedUser = new()
        {
            UserName = loginName
        };

        _userRepositoryMock.Setup(m => m.GetUser(It.IsAny<Func<IdentityUser, bool>>())).Returns((Func<IdentityUser, bool> predicate) =>
        {
            var userList = new List<IdentityUser>{ expectedUser };
            IdentityUser user = userList.SingleOrDefault(predicate);
            return user;
        });
        _userManagerMock.Setup(m => m.CheckPasswordAsync(expectedUser, loginPassword)).ReturnsAsync(true);

        // Act
        IdentityUser? actualUser = await _authService.LoginUserAsync(loginRequest);

        // Assert
        Assert.AreSame(expectedUser, actualUser);
    }

    [TestMethod]
    public async Task LoginUserAsync_ReturnsNullIfBadUser()
    {
        // Arrange
        LoginRequestDto loginRequest = new("loginName", "loginPassword");

        _userRepositoryMock.Setup(m => m.GetUser(It.IsAny<Func<IdentityUser, bool>>())).Returns(null as IdentityUser);

        // Act
        IdentityUser? actualUser = await _authService.LoginUserAsync(loginRequest);

        // Assert
        Assert.IsNull(actualUser);
    }

    [TestMethod]
    public async Task LoginUserAsync_ReturnsNullIfBadPassword()
    {
        LoginRequestDto loginRequest = new("loginName", "loginPassword");
        IdentityUser identityUser = new();

        _userRepositoryMock.Setup(m => m.GetUser(It.IsAny<Func<IdentityUser, bool>>())).Returns(identityUser);
        _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        IdentityUser? actualUser = await _authService.LoginUserAsync(loginRequest);

        // Assert
        Assert.IsNull(actualUser);
    }

    [TestMethod]
    public async Task RegisterUserAsync_RegistersUser()
    {
        // Arrange
        string registerName = "register name";
        string registerPassword = "register password";
        UserRole registerRole = UserRole.Player;
        RegisterRequestDto registerRequest = new(registerName, registerPassword, registerRole);

        _userManagerMock.Setup(m => m.CreateAsync(
            It.Is<IdentityUser>(u => u.UserName == registerName),
            registerPassword)
        ).ReturnsAsync(IdentityResult.Success);

        _roleManagerMock.Setup(m => m.RoleExistsAsync(registerRole.ToString())).ReturnsAsync(true);

        // Act
        IEnumerable<IdentityError>? actualResult = await _authService.RegisterUserAsync(registerRequest);

        // Assert
        Assert.IsNull(actualResult);

        _userManagerMock.Verify(m => m.AddToRoleAsync(
            It.Is<IdentityUser>(u => u.UserName == registerName),
            registerRole.ToString()
        ));
    }

    [TestMethod]
    public async Task RegisterUserAsync_CreatesRole()
    {
        // Arrange
        UserRole registerRole = UserRole.Player;
        RegisterRequestDto registerRequest = new("registerName", "registerPassword", registerRole);

        _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(m => m.RoleExistsAsync(registerRole.ToString())).ReturnsAsync(false);

        // Act
        await _authService.RegisterUserAsync(registerRequest);

        // Assert
        _roleManagerMock.Verify(m => m.CreateAsync(It.Is<IdentityRole>(r => r.Name == registerRole.ToString())));
    }

    [TestMethod]
    public async Task RegisterUserAsync_ReturnsErrors()
    {
        // Arrange
        RegisterRequestDto registerRequest = new("registerName", "registerPassword", UserRole.Player);
        IdentityError expectedError = new();

        _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(expectedError));

        // Act
        IEnumerable<IdentityError>? actualResult = await _authService.RegisterUserAsync(registerRequest);

        // Assert
        Assert.AreSame(expectedError, actualResult.Single());
    }
}