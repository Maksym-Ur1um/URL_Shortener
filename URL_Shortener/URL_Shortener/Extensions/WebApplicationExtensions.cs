using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;

namespace URL_Shortener.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();
            await DatabaseSeeder.SeedAsync(context);
        }
    }
}
