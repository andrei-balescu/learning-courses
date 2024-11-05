using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playstore.Auth.Service.Data;
using Playstore.Auth.Service.DataTransferObjects;

namespace Playstore.Auth.Service.Controllers;

[Route("user")]
[ApiController] 
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        IdentityUser user = new()
        {
            UserName = registerUserDto.Name
        };
        
        IdentityResult result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (result.Succeeded)
        {
            user = _dbContext.Users.Single(user => user.UserName == registerUserDto.Name);

            var registrationResponse = new UserDto(new Guid(user.Id), user.UserName);
            return Ok(registrationResponse);
        }
        else
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
}