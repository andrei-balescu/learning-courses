namespace Playstore.Auth.Contracts.DataTransferObjects;

public class RegisterResponseDto
{
    public bool IsSuccess { get; set; }

    public UserDto? User { get; set; }

    public string? ErrorMessage { get; set; }
}