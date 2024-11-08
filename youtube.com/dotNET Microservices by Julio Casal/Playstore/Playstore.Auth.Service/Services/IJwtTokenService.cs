using Microsoft.AspNetCore.Identity;

namespace Playstore.Auth.Service.Services;

/// <summary>Generates JWT tokens.</summary>
public interface IJwtTokenService
{
    /// <summary>Generate a JWT token.</summary>
    /// <param name="user">The user to generate token for.</param>
    /// <returns>The JWT token.</returns>
    Task<string> GenerateTokenAsync(IdentityUser user);
}