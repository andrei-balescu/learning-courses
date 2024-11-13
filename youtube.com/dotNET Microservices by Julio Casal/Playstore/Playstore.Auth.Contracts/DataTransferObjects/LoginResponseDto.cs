namespace Playstore.Auth.Contracts.DataTransferObjects;

/// <summary>The response of a successful login request.</summary>
/// <param name="User">User details.</param>
/// <param name="Token">The login token.</param>
public record LoginResponseDto(UserDto User, string Token);