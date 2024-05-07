using Microsoft.AspNetCore.Identity;
using N_Tier.Core.Entities.Identity;
using N_Tier.Core.Enums;

namespace N_Tier.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        if (!roleManager.Roles.Any())
        {
            foreach (var name in Enum.GetNames(typeof(ApplicationRole)))
            {
                await roleManager.CreateAsync(new IdentityRole(name));
            }
        }

        if (!userManager.Users.Any())
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                DateOfBirth = new DateTime(1980, 1, 1),
                Oib = "00000000000",
                IdCardNumber = "000000000"
            };

            await userManager.CreateAsync(admin, "Admin123.?");

            await userManager.AddToRoleAsync(admin, ApplicationRole.Administrator.ToString());

            var librarian = new ApplicationUser
            {
                UserName = "librarian@librarian.com",
                Email = "librarian@librarian.com",
                EmailConfirmed = true,
                FirstName = "Librarian",
                LastName = "Librarian",
                DateOfBirth = new DateTime(1990, 1, 1),
                Oib = "11111111111",
                IdCardNumber = "111111111"
            };

            await userManager.CreateAsync(librarian, "Librarian123.?");

            await userManager.AddToRoleAsync(librarian, ApplicationRole.Librarian.ToString());

            var customer = new ApplicationUser
            {
                UserName = "customer@customer.com",
                Email = "customer@customer.com",
                EmailConfirmed = true,
                FirstName = "Customer",
                LastName = "Customer",
                DateOfBirth = new DateTime(2000, 1, 1),
                Oib = "22222222222",
                IdCardNumber = "222222222"
            };

            await userManager.CreateAsync(customer, "Customer123.?");

            await userManager.AddToRoleAsync(customer, ApplicationRole.Customer.ToString());
        }

        await context.SaveChangesAsync();
    }
}
