using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Auth.Respositories;
using Playstore.Auth.Service.Services;

namespace Playstore.Auth.Service.Controllers;

/// <summary>Performs authentication / Authorization.</summary>
[Route("auth")]
[ApiController] 
public class AuthController : ControllerBase
{
    /// <summary>The service performing authentication / authorization.</summary>
    private readonly IAuthService _authService;

    /// <summary>Service for generating JWT tokens.</summary>
    private readonly IJwtTokenService _jwtTokenService;

    /// <summary>Service for managing users.</summary>
    private readonly IUserRepository _userRepository;

    /// <summary>Create a new instance.</summary>
    /// <param name="authService">The service performing authentication / authorization.</param>
    /// <param name="jwtTokenService">Service for generating JWT tokens.</param>
    /// <param name="userRepository">Service for managing users.</param>
    public AuthController(IAuthService authService, IJwtTokenService jwtTokenService, IUserRepository userRepository)
    {
        _authService = authService;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }

    /// <summary>Registers a user.</summary>
    /// <param name="registerUserDto">The registration parameters.</param>
    /// <returns>The registration result.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequestDto registerUserDto)
    {
        IEnumerable<IdentityError>? errors = await _authService.RegisterUserAsync(registerUserDto);
        if (errors == null)
        {
            IdentityUser user = _userRepository.GetUser(u => u.UserName == registerUserDto.Name);

            UserDto registrationResponse = new UserDto(new Guid(user.Id), user.UserName);
            return Ok(registrationResponse);
        }
        else
        {
            return BadRequest(errors);
        }
    }

    /// <summary>Logs in a user.</summary>
    /// <param name="loginUserDto">The login parameters.</param>
    /// <returns>Ok if the login was successful.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequestDto loginUserDto)
    {
        IdentityUser? user = await _authService.LoginUserAsync(loginUserDto);
        if (user == null)
        {
            return BadRequest();
        }

        string token = await _jwtTokenService.GenerateTokenAsync(user);
        LoginResponseDto loginResponse = new(
            new UserDto(new Guid(user.Id), user.UserName),
            token
        );
        return Ok(loginResponse);
    }

    /// <summary>Returns identity errors in a Bad Request response.</summary>
    /// <param name="identityErrors">The errors to return.</param>
    /// <returns>A Bad Request response containing errors.</returns>
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