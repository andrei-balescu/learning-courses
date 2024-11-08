using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Auth;
using Playstore.Client.ServiceClients;
using Playstore.Client.Services;

namespace Playstore.Client.Controllers;

/// <summary>Authentication / authorization controller.</summary>
public class AuthController : Controller
{
    /// <summary>Client to use for performing authentication / authorization.</summary>
    private readonly IAuthClient _authClient;

    private readonly IJwtTokenService _jwtTokenService;

    /// <summary>Create a new instance.</summary>
    /// <param name="authClient">Client to use for performing authentication / authorization.</param>
    public AuthController(IAuthClient authClient, IJwtTokenService jwtTokenService)
    {
        _authClient = authClient;
        _jwtTokenService = jwtTokenService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            LoginResponseDto loginResponse = await _authClient.LoginUserAsync(new LoginRequestDto(login.UserName, login.Password));

            if (loginResponse != null)
            {
                ClaimsPrincipal principal = _jwtTokenService.GetPrincipal(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    loginResponse.token);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError("(none)", "Username or password incorrect.");
        return View(login);
    }

    /// <summary>Page to register a user.</summary>
    /// <returns>The Auth/Register page</returns>
    [HttpGet]
    public IActionResult Register()
    {
        AddRolesToView();
        return View();
    }

    /// <summary>Register a user.</summary>
    /// <param name="registration">Registration parameters.</param>
    /// <returns>Redirects to Auth/Login page if registration successful.</returns>
    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel registration)
    {
        if (ModelState.IsValid)
        {
            RegisterResponseDto registerResponse = await _authClient.RegisterUserAsync(new RegisterRequestDto(
                registration.UserName,
                registration.Password,
                registration.Role));

            if (registerResponse.IsSuccess)
            {
                TempData[NotificationsViewModel.c_Success] = $"User {registerResponse.User.Name} registered successfully";
                return RedirectToAction("Login");
            }
            else if (registerResponse.BadRequest != null)
            {
                foreach(KeyValuePair<string, IEnumerable<string>> field in registerResponse.BadRequest.Errors)
                {
                    ModelState.AddModelError(field.Key, field.Value.First());
                }
            }
        }
        
        AddRolesToView();
        return View(registration);
    }

    private void AddRolesToView()
    {
        var roleList = new List<SelectListItem>
        {
            new SelectListItem { Text = UserRole.Player.ToString(), Value = ((int)UserRole.Player).ToString() },
            new SelectListItem { Text = UserRole.GameMaster.ToString(), Value = ((int)UserRole.GameMaster).ToString() }
        };

        ViewBag.RoleList = roleList;
    }

    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}