namespace Playstore.Auth.Contracts.DataTransferObjects;

/// <summary>The response of a successful login request.</summary>
/// <param name="user">User details.</param>
/// <param name="token">The login token.</param>
public record LoginResponseDto(UserDto user, string token);