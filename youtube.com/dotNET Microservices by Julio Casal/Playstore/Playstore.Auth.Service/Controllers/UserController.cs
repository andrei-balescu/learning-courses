using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Playstore.Auth.Service.DataTransferObjects;
using Playstore.Auth.Service.Services;

namespace Playstore.Auth.Service.Controllers;

[Route("user")]
[ApiController] 
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IJwtTokenService _jwtTokenService;

    public UserController(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerUserDto)
    {
        IEnumerable<IdentityError>? result = await _userService.RegisterUser(registerUserDto);
        if (result == null)
        {
            IdentityUser user = _userService.GetUserByName(registerUserDto.Name);

            var registrationResponse = new UserDto(new Guid(user.Id), user.UserName);
            return Ok(registrationResponse);
        }
        
        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginUserDto)
    {
        IdentityUser? user = await _userService.LoginUser(loginUserDto);
        if (user == null)
        {
            return BadRequest();
        }

        string token = _jwtTokenService.GenerateToken(user);
        LoginResponseDto loginResponse = new(
            new UserDto(new Guid(user.Id), user.UserName),
            token
        );
        return Ok(loginResponse);
    }

    private BadRequestObjectResult BadRequest(IEnumerable<IdentityError> identityErrors)
    {
        foreach(var error in identityErrors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        // make behavior consistent with default API validation
        var validationProblems = new ValidationProblemDetails(ModelState);
        validationProblems.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        return BadRequest(validationProblems);
    }
}