using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Service.DataTransferObjects;

public record RegisterUserRequest(
    [Required]
    string Name, 
    
    [Required]
    string Password);