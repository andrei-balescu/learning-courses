using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Service.DataTransferObjects;

public record LoginRequestDto(
    [Required]
    string Name,

    [Required]
    string Password
);