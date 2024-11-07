using System.ComponentModel.DataAnnotations;
using Playstore.Auth.Contracts.DataTransferObjects;

namespace Playstore.Client.Models.Auth;

/// <summary>View model for the registration page.</summary>
public class RegisterViewModel
{
    /// <summary>Registration user name.</summary>
    /// <remarks>See regex info: https://stackoverflow.com/questions/12018245/regular-expression-to-validate-username</remarks>
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    /// <summary>Registration password.</summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage="Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public UserRole Role { get; set; }
}