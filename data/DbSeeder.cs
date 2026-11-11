using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgressReport.models;

namespace ProgressReport.data;

public class DbSeeder(
    AppDbContext db,
    IPasswordHasher<User> passwordHasher,
    IConfiguration config,
    ILogger<DbSeeder> logger)
{
    public async Task SeedAsync()
    {
        if (await db.Users.AnyAsync())
            return;

        var email = config["Seed:AdminEmail"];
        var password = config["Seed:AdminPassword"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogWarning("Seed:AdminEmail / Seed:AdminPassword non définis : aucun utilisateur par défaut créé.");
            return;
        }

        var admin = new User
        {
            FullName = config["Seed:AdminFullName"] ?? "Administrateur",
            Email = email,
            Role = UserRole.ADMIN
        };
        admin.Password = passwordHasher.HashPassword(admin, password);

        db.Users.Add(admin);
        await db.SaveChangesAsync();
        logger.LogInformation("Utilisateur par défaut créé : {Email}", email);
    }
}