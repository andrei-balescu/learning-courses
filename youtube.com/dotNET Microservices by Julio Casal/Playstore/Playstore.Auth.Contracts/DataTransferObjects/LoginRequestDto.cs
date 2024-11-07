using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Contracts.DataTransferObjects;

public record LoginRequestDto(
    [Required]
    string Name,

    [Required]
    string Password
);