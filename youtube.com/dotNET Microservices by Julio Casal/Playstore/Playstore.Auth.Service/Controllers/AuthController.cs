using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Service.Services;

namespace Playstore.Auth.Service.Controllers;

/// <summary>Performs authentication / Authorization.</summary>
[Route("auth")]
[ApiController] 
public class AuthController : ControllerBase
{
    /// <summary>The service performing authentication / authorization.</summary>
    private readonly IAuthService _userService;

    /// <summary>Service for generating JWT tokens.</summary>
    private readonly IJwtTokenService _jwtTokenService;

    /// <summary>Create a new instance.</summary>
    /// <param name="userService">The service performing authentication / authorization.</param>
    /// <param name="jwtTokenService">Service for generating JWT tokens.</param>
    public AuthController(IAuthService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    /// <summary>Registers a user.</summary>
    /// <param name="registerUserDto">The registration parameters.</param>
    /// <returns>The registration result.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerUserDto)
    {
        RegisterResponseDto registrationResponse;

        IEnumerable<IdentityError>? result = await _userService.RegisterUser(registerUserDto);
        if (result == null)
        {
            IdentityUser user = _userService.GetUserByName(registerUserDto.Name);

            registrationResponse = new()
            {
                IsSuccess = true,
                User = new UserDto(new Guid(user.Id), user.UserName)
            };
            return Ok(registrationResponse);
        }
        else
        {
            registrationResponse = new()
            {
                IsSuccess = false,
                ErrorMessage = result.First().Description
            };

            return BadRequest(registrationResponse);
        }
    }

    /// <summary>Logs in a user.</summary>
    /// <param name="loginUserDto">The login parameters.</param>
    /// <returns>Ok if the login was successful.</returns>
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
}