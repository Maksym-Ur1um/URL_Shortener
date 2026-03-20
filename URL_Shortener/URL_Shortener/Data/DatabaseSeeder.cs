using Microsoft.AspNetCore.Identity;
using URL_Shortener.Models;

namespace URL_Shortener.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new Role { Name = "Admin" });
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new Role { Name = "User" });
            }

            var admin = await userManager.FindByNameAsync("Admin");
            if (admin == null)
            {
                User userAdmin = new User { UserName = "Admin" };
                User userIvan = new User { UserName = "Ivan" };
                User userMaksym = new User { UserName = "Maksym" };

                var result = await userManager.CreateAsync(userAdmin, "Admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userAdmin, "Admin");
                }
                result = await userManager.CreateAsync(userIvan, "Qwerty123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userIvan, "User");
                }
                result = await userManager.CreateAsync(userMaksym, "Qwerty123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userMaksym, "User");
                }
            }
        }
    }
}