using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Contracts.DataTransferObjects;

/// <summary>Login request parameters.</summary>
/// <param name="Name">The login name.</param>
/// <param name="Password">The user password.</param>
public record LoginRequestDto(
    [Required]
    string Name,

    [Required]
    string Password
);