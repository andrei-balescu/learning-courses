using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Service.Data;
namespace Playstore.Auth.Service.Services;

/// <summary>Service for performing authentication / authorization.</summary>
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IUserService _userService;

    public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userService = userService;
    }

    /// <summary>Logs in a user.</summary>
    /// <param name="loginRequestDto">User login details.</param>
    /// <returns>The logged in user.</returns>
    public async Task<IdentityUser?> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        IdentityUser? user = _userService.GetUser(u => u.UserName == loginRequestDto.Name);
        if (user != null)
        {
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (isValid)
            {
                return user;
            }
        }

        return null;
    }

    /// <summary>Register a user.</summary>
    /// <param name="registerRequestDto">User registration parameters.</param>
    /// <returns>Any errors encountered during registration.</returns>
    public async Task<IEnumerable<IdentityError>?> RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        IdentityUser user = new()
        {
            UserName = registerRequestDto.Name
        };
        
        IdentityResult result = await _userManager.CreateAsync(user, registerRequestDto.Password);

        if (!result.Succeeded)
        {
            return result.Errors;
        }
        else
        {
            if (registerRequestDto.role != UserRole.None)
            {
                await AssignRoleAsync(user, registerRequestDto.role);
            }

            return null;
        }
    }

    private async Task AssignRoleAsync(IdentityUser user, UserRole role)
    {
        var roleAsString = role.ToString();
        if (!await _roleManager.RoleExistsAsync(roleAsString))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleAsString));
        }

        await _userManager.AddToRoleAsync(user, roleAsString);
    }
}