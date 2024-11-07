using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Auth;
using Playstore.Client.ServiceClients;

namespace Playstore.Client.Controllers;

public class AuthController : Controller
{
    private readonly IAuthClient _authClient;

    public AuthController(IAuthClient authClient)
    {
        _authClient = authClient;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        AddRolesToView();
        return View();
    }

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
                return RedirectToAction("Index", "Home");
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