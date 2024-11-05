using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Service.DataTransferObjects;

public record LoginUserDto(
    [Required]
    string Name,

    [Required]
    string Password
);