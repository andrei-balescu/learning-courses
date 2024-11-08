using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Auth;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for communicating with the auth service.</summary>
public interface IAuthClient
{
    /// <summary>Register a user. </summary>
    /// <param name="registerRequestDto">Registration request details.</param>
    /// <returns>The registered user or an error if any.</returns>
    Task<RegisterResponseDto> RegisterUserAsync(RegisterRequestDto registerRequestDto);

    Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto);
}