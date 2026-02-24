using installer_site_SK.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace installer_site_SK.Controllers;

[Authorize(Roles = "Admin,SuperAdmin")]
[Route("admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var vm = new List<UserListItemViewModel>();

        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            vm.Add(new UserListItemViewModel
            {
                Email = u.Email ?? u.UserName ?? "",
                Roles = roles.ToList()
            });
        }

        return View(vm);
    }

    [HttpGet("create-user")]
    public IActionResult CreateUser() => View(new CreateUserViewModel());

    [HttpPost("create-user")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Admin не может создавать Admin или SuperAdmin
        if (User.IsInRole("Admin") && model.Role != "Worker")
        {
            ModelState.AddModelError("", "Вы не можете создавать администраторов.");
            return View(model);
        }

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true,
            LockoutEnabled = true

        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var passwordErrorCodes = new HashSet<string>
            {
                "PasswordTooShort",
                "PasswordRequiresDigit",
                "PasswordRequiresUpper",
                "PasswordRequiresLower",
                "PasswordRequiresNonAlphanumeric",
                "PasswordRequiresUniqueChars"
            };


            var hasPasswordErrors = result.Errors.Any(e => passwordErrorCodes.Contains(e.Code));

            if (hasPasswordErrors)
            {
                ModelState.AddModelError("", "Пароль должен быть не короче 8 символов и содержать: 1 заглавную букву, 1 цифру и 1 спецсимвол.");
            }
            else
            {
                // Остальные ошибки показываем как есть (они уже русифицированы describer’ом)
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            ModelState.AddModelError("", $"Роль '{model.Role}' не найдена в системе.");
            return View(model);
        }

        await _userManager.AddToRoleAsync(user, model.Role);

        return RedirectToAction(nameof(Index));
    }
}