using Microsoft.AspNetCore.Identity;

namespace installer_site_SK.Data.Seed;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = ["SuperAdmin", "Admin", "Worker"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // SuperAdmin
        var superEmail = "buzlov@sk.com";
        var superPassword = "John_Ram-4991";

        var user = await userManager.FindByEmailAsync(superEmail);
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = superEmail,
                Email = superEmail,
                EmailConfirmed = true
            };

            var createRes = await userManager.CreateAsync(user, superPassword);
            if (!createRes.Succeeded)
            {
                var errors = string.Join("; ", createRes.Errors.Select(e => $"{e.Code}:{e.Description}"));
                throw new Exception("Cannot create SuperAdmin: " + errors);
            }
        }

        if (!await userManager.IsInRoleAsync(user, "SuperAdmin"))
        {
            await userManager.AddToRoleAsync(user, "SuperAdmin");
        }
    }
}