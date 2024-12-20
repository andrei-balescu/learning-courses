using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Playstore.Common.Settings;

namespace Playstore.Client.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>Validates a JWT token and creates a principal from it.</summary>
    /// <param name="authenticationScheme">The authentication scheme to use.</param>
    /// <param name="jwtToken">The JWT token.</param>
    /// <returns>The principal.</returns>
    public ClaimsPrincipal GetPrincipal(string authenticationScheme, string jwtToken)
    {
        ValidateToken(jwtToken);
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwt = handler.ReadJwtToken(jwtToken);

        var identity = new ClaimsIdentity(authenticationScheme);
        identity.AddClaim(jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId));
        identity.AddClaim(jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub));

        // ClaimTypes REQUIRED for ASP.NET authorization integration.
        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(c => c.Type == "role")?.Value));

        var principal = new ClaimsPrincipal(identity);
        return principal;
    }

    /// <summary>Validates a JWT token.</summary>
    /// <param name="token">The token to validate.</param>
    private void ValidateToken(string token)
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
        tokenHandler.ValidateToken(token, validationParameters, out SecurityToken? validatedToken);
    }
}