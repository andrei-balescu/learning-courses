using System.Security.Claims;

namespace Playstore.Client.Services;

public interface IJwtTokenService
{
    /// <summary>Validates a JWT token and creates a principal from it.</summary>
    /// <param name="authenticationScheme">The authentication scheme to use.</param>
    /// <param name="jwtToken">The JWT token.</param>
    /// <returns>The principal.</returns>
    ClaimsPrincipal GetPrincipal(string authenticationScheme, string jwtToken);
}