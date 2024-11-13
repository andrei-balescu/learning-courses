using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Services;
using Playstore.Common.Settings;

namespace Playstore.Client.Test.Services;

[TestClass]
public class JwtTokenServiceTests
{
    private JwtSettings _jwtSettings;

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
        var jwtSettingsMock = new Mock<IOptions<JwtSettings>>();
        jwtSettingsMock.SetupGet(m => m.Value).Returns(_jwtSettings);

        _jwtTokenService = new JwtTokenService(jwtSettingsMock.Object);
    }

    [TestMethod]
    public void GetPrincipal_ValidatesAudience()
    {
        // Arrange
        var jwtSettings = new JwtSettings
        {
            Issuer = _jwtSettings.Issuer,
            Audience = "invalid audience",
            Secret = _jwtSettings.Secret
        };

        string token = GenerateToken(new List<Claim>(), jwtSettings);

        bool isExpectedError = false;
        // Act
        try
        {
            _jwtTokenService.GetPrincipal("auth scheme", token);
        }
        catch (SecurityTokenInvalidAudienceException)
        {
            isExpectedError = true;
        }

        // Assert
        Assert.IsTrue(isExpectedError);
    }

    [TestMethod]
    public void GetPrincipal_ValidatesIssuer()
    {
        // Arrange
        var jwtSettings = new JwtSettings
        {
            Issuer = "invalid issuer",
            Audience = _jwtSettings.Audience,
            Secret = _jwtSettings.Secret
        };

        string token = GenerateToken(new List<Claim>(), jwtSettings);

        bool isExpectedError = false;
        // Act
        try
        {
            _jwtTokenService.GetPrincipal("auth scheme", token);
        }
        catch (SecurityTokenInvalidIssuerException)
        {
            isExpectedError = true;
        }

        // Assert
        Assert.IsTrue(isExpectedError);
    }
    
    [TestMethod]
    public void GetPrincipal_ValidatesSigningKey()
    {
        // Arrange
        var jwtSettings = new JwtSettings
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Secret = "invalid secret: Lorem ipsum dolor sit amet, consectetur adipiscing elit"
        };

        string token = GenerateToken(new List<Claim>(), jwtSettings);

        bool isExpectedError = false;
        // Act
        try
        {
            _jwtTokenService.GetPrincipal("auth scheme", token);
        }
        catch (SecurityTokenInvalidSignatureException)
        {
            isExpectedError = true;
        }

        // Assert
        Assert.IsTrue(isExpectedError);
    }

    [TestMethod]
    public void GetPrincipal_GetsClaims()
    {
        // Arrange
        string expectedAuthScheme = "auth scheme";
        string expectedUserName = "test user name";
        var expectedUserId = Guid.NewGuid();
        var expectedRole = UserRole.Player;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, expectedUserName),
            new Claim(JwtRegisteredClaimNames.Sub, expectedUserId.ToString()),
            new Claim(ClaimTypes.Role, expectedRole.ToString())
        };

        var token = GenerateToken(claims);

        ClaimsPrincipal principal = _jwtTokenService.GetPrincipal(expectedAuthScheme, token);
        Assert.AreEqual(principal.Identity.AuthenticationType, expectedAuthScheme);

        Assert.AreEqual(expectedUserName, principal.Claims.Single(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
        Assert.AreEqual(expectedUserId.ToString(), principal.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
        Assert.AreEqual(expectedRole.ToString(), principal.Claims.Single(c => c.Type == ClaimTypes.Role).Value);
    }

    public string GenerateToken(IEnumerable<Claim> claims, JwtSettings? jwtSettings = null)
    {
        byte[] key = Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? _jwtSettings.Secret);
        
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Audience = jwtSettings?.Audience ?? _jwtSettings.Audience,
            Issuer = jwtSettings?.Issuer ?? _jwtSettings.Issuer,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}