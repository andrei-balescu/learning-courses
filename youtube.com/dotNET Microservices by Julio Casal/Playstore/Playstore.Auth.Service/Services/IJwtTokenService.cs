using Microsoft.AspNetCore.Identity;

namespace Playstore.Auth.Service.Services;

public interface IJwtTokenService
{
    string GenerateToken(IdentityUser user);
}