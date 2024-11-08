using Playstore.Auth.Contracts.DataTransferObjects;

namespace Playstore.Client.Models.Auth;

/// <summary>Registration response DTO.</summary>
public class RegisterResponseDto
{
    /// <summary>Wether the registration was successful.</summary>
    public bool IsSuccess { get; set; }

    /// <summary>The user details.</summary>
    public UserDto? User { get; set; }

    /// <summary>Errors encountered when registering user if any.</summary>
    public BadRequestDto? BadRequest { get; set; }
}