using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Service.DataTransferObjects;

public record RegisterUserDto(
    [Required]
    string Name, 
    
    [Required]
    string Password);