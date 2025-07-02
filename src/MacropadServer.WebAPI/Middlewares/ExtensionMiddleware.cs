using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.WebAPI.Middlewares;

public static class ExtensionMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.Email == "eren@gmail.com"))
            {
                AppUser user = new()
                {
                    UserName = "eren",
                    Email = "eren@gmail.com",
                    FirstName = "Eren",
                    LastName = "Delibaş",
                    EmailConfirmed = true,
                    SecretToken = Guid.NewGuid().ToString(),
                    Role = UserRoleEnum.User,
                    CreatedAt = DateTimeOffset.UtcNow,
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }

    public static void CreateAdmin(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.Email == "info@erendelibas.com"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "info@erendelibas.com",
                    FirstName = "Eren",
                    LastName = "Delibaş",
                    EmailConfirmed = true,
                    SecretToken = Guid.NewGuid().ToString(),
                    Role = UserRoleEnum.Admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    CreatedBy = "admin"
                };

                userManager.CreateAsync(user, "Password123*").Wait();
            }
        }
    }
}
