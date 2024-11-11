using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Service.Data;
namespace Playstore.Auth.Service.Services;

/// <summary>Service for performing authentication / authorization.</summary>
public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>Gets a user by login name.</summary>
    /// <param name="loginName">The login name.</param>
    /// <returns>The user.</returns>
    public IdentityUser GetUserByName(string userId)
    {
        IdentityUser? user = _dbContext.Users.Single(u => u.UserName == userId);
        return user;
    }

    /// <summary>Logs in a user.</summary>
    /// <param name="loginRequestDto">User login details.</param>
    /// <returns>The logged in user.</returns>
    public async Task<IdentityUser?> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        IdentityUser? user = _dbContext.Users.SingleOrDefault(u => u.UserName == loginRequestDto.Name);
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