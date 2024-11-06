using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Service.Data;
using Playstore.Auth.Service.DataTransferObjects;

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

    public async Task<IdentityUser?> LoginUser(LoginUserDto loginUserDto)
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

    public async Task<IEnumerable<IdentityError>?> RegisterUser(RegisterUserDto registerUserDto)
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
            return null;
        }
    }
}