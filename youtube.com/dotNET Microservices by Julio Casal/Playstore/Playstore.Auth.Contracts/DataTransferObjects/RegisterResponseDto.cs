namespace Playstore.Auth.Contracts.DataTransferObjects;

/// <summary>Registration response DTO.</summary>
public class RegisterResponseDto
{
    /// <summary>Wether the registration was successful.</summary>
    public bool IsSuccess { get; set; }

    /// <summary>The user details.</summary>
    public UserDto? User { get; set; }

    /// <summary>Error encountered when registering user.</summary>
    public string? ErrorMessage { get; set; }
}