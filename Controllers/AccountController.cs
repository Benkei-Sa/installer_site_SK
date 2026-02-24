using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace installer_site_SK.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Введите почту и пароль.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError("", "Неверная почта или пароль.");
            return View();
        }
        bool isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            password,
            isPersistent: true,
            lockoutOnFailure: !isSuperAdmin
        );
        
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", "Слишком много попыток. Попробуйте через 3 минуты.");
            return View();
        }
        
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Неверная почта или пароль.");
            return View();
        }

        // если returnUrl задан и он локальный — идём туда
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        // иначе — редирект по роли
        if (await _userManager.IsInRoleAsync(user, "SuperAdmin") ||
            await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return Redirect("/admin");
        }

        return Redirect("/m/orders");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}