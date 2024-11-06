namespace Playstore.Auth.Service.DataTransferObjects;

public record LoginResponseDto(UserDto user, string token);