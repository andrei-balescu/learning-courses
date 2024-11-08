using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Playstore.Auth.Service.Settings;

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
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwt = handler.ReadJwtToken(jwtToken);

        ClaimsIdentity identity = new(authenticationScheme);
        identity.AddClaim(jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId));
        identity.AddClaim(jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub));

        ClaimsPrincipal principal = new(identity);
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
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
        };

        JwtSecurityTokenHandler tokenHandler = new();
        tokenHandler.ValidateToken(token, validationParameters, out SecurityToken? validatedToken);
    }
}