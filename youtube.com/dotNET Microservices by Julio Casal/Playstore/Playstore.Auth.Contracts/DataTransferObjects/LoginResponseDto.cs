namespace Playstore.Auth.Contracts.DataTransferObjects;

public record LoginResponseDto(UserDto user, string token);