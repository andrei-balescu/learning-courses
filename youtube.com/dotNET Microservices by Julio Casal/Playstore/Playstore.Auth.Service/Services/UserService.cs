using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Service.Data;
namespace Playstore.Auth.Service.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IdentityUser GetUserByName(string userId)
    {
        IdentityUser? user = _dbContext.Users.Single(u => u.UserName == userId);
        return user;
    }

    public async Task<IdentityUser?> LoginUser(LoginRequestDto loginUserDto)
    {
        IdentityUser? user = _dbContext.Users.SingleOrDefault(u => u.UserName == loginUserDto.Name);
        if (user != null)
        {
            bool isValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (isValid)
            {
                return user;
            }
        }

        return null;
    }

    public async Task<IEnumerable<IdentityError>?> RegisterUser(RegisterRequestDto registerUserDto)
    {
        IdentityUser user = new()
        {
            UserName = registerUserDto.Name
        };
        
        IdentityResult result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
        {
            return result.Errors;
        }
        else
        {
            if (registerUserDto.role != UserRole.None)
            {
                await AssignRole(user, registerUserDto.role);
            }

            return null;
        }
    }

    private async Task AssignRole(IdentityUser user, UserRole role)
    {
        var roleAsString = role.ToString();
        if (!await _roleManager.RoleExistsAsync(roleAsString))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleAsString));
        }

        await _userManager.AddToRoleAsync(user, roleAsString);
    }
}