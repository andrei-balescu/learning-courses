using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Playstore.Auth.Service.Settings;

namespace Playstore.Auth.Service.Services;

/// <summary>Generates JWT tokens.</summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings, UserManager<IdentityUser> roleManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = roleManager;
    }

    /// <summary>Generate a JWT token.</summary>
    /// <param name="user">The user to generate token for.</param>
    /// <returns>The JWT token.</returns>
    public async Task<string> GenerateTokenAsync(IdentityUser user)
    {
        byte[] key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

        var claims = await GetClaims(user);
        
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    /// <summary>Gets claims for a user.</summary>
    /// <param name="user">The user to get claims for.</param>
    /// <returns>A list of claims</returns>
    private async Task<IEnumerable<Claim>> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            // Unique ID for the token
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        IList<string> roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}