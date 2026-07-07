using ConstructionExpenseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionExpenseManager.Api.Data;

// Seeds the fixed 5 users (1 SuperAdmin + 4 Managers) on first run.
// Edit the placeholder mobile numbers below (or update via the Users API afterwards).
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await db.Database.MigrateAsync();

        if (await db.Users.AnyAsync())
        {
            return;
        }

        db.Users.AddRange(
            new User { Name = "Super Admin", MobileNumber = "8669061895", Role = UserRole.SuperAdmin, IsActive = true },
            new User { Name = "Manager 1", MobileNumber = "9000000002", Role = UserRole.Manager, IsActive = true },
            new User { Name = "Manager 2", MobileNumber = "9000000003", Role = UserRole.Manager, IsActive = true },
            new User { Name = "Manager 3", MobileNumber = "9000000004", Role = UserRole.Manager, IsActive = true },
            new User { Name = "Manager 4", MobileNumber = "9000000005", Role = UserRole.Manager, IsActive = true }
        );

        await db.SaveChangesAsync();
    }
}
