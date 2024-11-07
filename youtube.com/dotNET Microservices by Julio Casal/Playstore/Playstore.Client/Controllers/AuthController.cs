using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Auth;
using Playstore.Client.ServiceClients;

namespace Playstore.Client.Controllers;

/// <summary>Authentication / authorization controller.</summary>
public class AuthController : Controller
{
    /// <summary>Client to use for performing authentication / authorization.</summary>
    private readonly IAuthClient _authClient;

    /// <summary>Create a new instance.</summary>
    /// <param name="authClient">Client to use for performing authentication / authorization.</param>
    public AuthController(IAuthClient authClient)
    {
        _authClient = authClient;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
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
    public async Task<IActionResult> Register(RegisterViewModel registration)
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
            else
            {
                ModelState.AddModelError("(none)", registerResponse.ErrorMessage);
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
            new SelectListItem { Text = UserRole.GameMaster.ToString(), Value = ((int)UserRole.GameMaster).ToString() },
            new SelectListItem { Text = UserRole.Admin.ToString(), Value = ((int)UserRole.Admin).ToString() }
        };

        ViewBag.RoleList = roleList;
    }

    public IActionResult Logout()
    {
        return View();
    }
}