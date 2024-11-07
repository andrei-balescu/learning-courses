using System.ComponentModel.DataAnnotations;

namespace Playstore.Client.Models.Auth;

/// <summary>View model for the login page.</summary>
public class LoginViewModel
{
    /// <summary>Login user name.</summary>
    [Required]
    public string UserName { get; set; }

    /// <summary>Login password.</summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}