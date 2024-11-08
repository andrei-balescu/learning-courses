using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Contracts.DataTransferObjects;

/// <summary>Registration request parameters.</summary>
/// <param name="Name">The user login name.</param>
/// <param name="Password">The user password.</param>
/// <param name="role">The user role.</param>
public record RegisterRequestDto(
    [Required]
    string Name, 
    
    [Required]
    string Password,
    
    UserRole role);