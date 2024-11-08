using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Contracts.DataTransferObjects;

namespace Playstore.Auth.Service.Services;

/// <summary>Service for performing authentication / authorization.</summary>
public interface IAuthService
{
    /// <summary>Register a user.</summary>
    /// <param name="registerUserDto">User registration parameters.</param>
    /// <returns>Any errors encountered during registration.</returns>
    Task<IEnumerable<IdentityError>?> RegisterUser(RegisterRequestDto registerUserDto);

    /// <summary>Logs in a user.</summary>
    /// <param name="user">User login details.</param>
    /// <returns>The logged in user.</returns>
    Task<IdentityUser?> LoginUser(LoginRequestDto user);

    /// <summary>Gets a user by login name.</summary>
    /// <param name="loginName">The login name.</param>
    /// <returns>The user.</returns>
    IdentityUser GetUserByName(string loginName);
}