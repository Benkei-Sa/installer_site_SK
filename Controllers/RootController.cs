using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace installer_site_SK.Controllers;

public class RootController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public RootController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
            return Redirect("/Account/Login");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Redirect("/Account/Login");

        if (await _userManager.IsInRoleAsync(user, "SuperAdmin") ||
            await _userManager.IsInRoleAsync(user, "Admin"))
            return Redirect("/admin");

        return Redirect("/m/orders");
    }
}