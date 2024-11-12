using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Service.Services;
using Playstore.Common.Settings;

namespace Playstore.Auth.Test.Services;

[TestClass]
public class JwtTokenServiceTests
{
    private JwtSettings _jwtSettings;

    private Mock<UserManager<IdentityUser>> _userManagerMock;

    private JwtTokenService _jwtTokenService;

    [TestInitialize]
    public void TestInitialize()
    {
        _jwtSettings = new JwtSettings
        {
            Issuer = "token issuer",
            Audience = "token audience",
            Secret = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
        };
        Mock<IOptions<JwtSettings>> jwtSettingsMock = new();
        jwtSettingsMock.SetupGet(m => m.Value).Returns(_jwtSettings);

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

        _jwtTokenService = new JwtTokenService(jwtSettingsMock.Object, _userManagerMock.Object);
    }

    [TestMethod]
    public async Task GenerateTokenAsync_GeneratesCorrectToken()
    {
        // Arrange
        string expectedUserId = "userId";
        string expectedUserName = "userName";
        UserRole expectedRole = UserRole.Player;

        var identityUser = new IdentityUser
        {
            Id = expectedUserId,
            UserName = expectedUserName
        };

        _userManagerMock.Setup(m => m.GetRolesAsync(identityUser)).ReturnsAsync(new List<string> { expectedRole.ToString() });

        // Act
        string jwtToken = await _jwtTokenService.GenerateTokenAsync(identityUser);

        // Assert
        ValidateJwtToken(jwtToken);

        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwt = handler.ReadJwtToken(jwtToken);

        string actualUserId = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
        Assert.AreEqual(expectedUserId, actualUserId);

        string actualUserName = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value;
        Assert.AreEqual(expectedUserName, actualUserName);

        string actualUserRole = jwt.Claims.FirstOrDefault(c => c.Type == "role").Value;
        Assert.AreEqual(expectedRole.ToString(), actualUserRole);
    }

    private void ValidateJwtToken(string jwtToken)
    {
        TokenValidationParameters validationParameters = new()
        {
            ValidateLifetime = true, // default
            ValidateIssuer = true, // default
            ValidateAudience = true, // default
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
        };

        JwtSecurityTokenHandler tokenHandler = new();
        tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken? validatedToken);
    }
}