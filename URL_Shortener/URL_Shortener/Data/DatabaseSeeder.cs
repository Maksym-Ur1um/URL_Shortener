using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.Users.AnyAsync())
            {
                string hashedUserPassword = BCrypt.Net.BCrypt.HashPassword("qwerty");
                string hashedAdminPassword = BCrypt.Net.BCrypt.HashPassword("admin123");

                await context.Users.AddRangeAsync(
                    new User { UserName = "Ivan", PasswordHash = hashedUserPassword, Role = User.RoleTitle.OrdinaryUser },
                    new User { UserName = "Admin", PasswordHash = hashedAdminPassword, Role = User.RoleTitle.Admin },
                    new User { UserName = "Maksym", PasswordHash = hashedUserPassword, Role = User.RoleTitle.OrdinaryUser }
                );
            }

            if (!await context.PageContents.AnyAsync())
            {
                await context.PageContents.AddAsync(
                    new PageContent
                    {
                        PageName = "About",
                        TextContent = "This application uses a highly efficient mathematical approach called Base62 Encoding. When you submit a long URL, the system first securely saves it into database and assigns it a unique numeric ID. We then take this standard base-10 numeric ID and convert it into a Base62 string. The Base62 alphabet consists of 62 characters: lowercase letters (a-z), uppercase letters (A-Z), and digits (0-9). This conversion guarantees that every generated short link is absolutely unique, collision-free, and as short as mathematically possible."
                    }
                );
            }
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}

