using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Service.DataTransferObjects;

public record RegisterRequestDto(
    [Required]
    string Name, 
    
    [Required]
    string Password,
    
    UserRole role);