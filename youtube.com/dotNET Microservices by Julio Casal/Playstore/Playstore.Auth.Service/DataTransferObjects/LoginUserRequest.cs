using System.ComponentModel.DataAnnotations;

namespace Playstore.Auth.Service.DataTransferObjects;

public record LoginUserRequest(
    [Required]
    string Name,

    [Required]
    string Password
);