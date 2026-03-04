using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        public DbSet<PageContent> PageContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>()
                .HasIndex(u => u.OriginalUrl)
                .IsUnique();

            modelBuilder.Entity<ShortenedUrl>()
                .HasIndex(u => u.ShortUrl)
                .IsUnique();

            string hashedUserPassword = BCrypt.Net.BCrypt.HashPassword("qwerty");
            string hashedAdminPassword = BCrypt.Net.BCrypt.HashPassword("admin123");

            modelBuilder.Entity<User>().HasData(

                new User { Id = 1, UserName = "Ivan", PasswordHash = hashedUserPassword, Role = User.RoleTitle.OrdinaryUser },
                new User { Id = 2, UserName = "Admin", PasswordHash = hashedAdminPassword, Role = User.RoleTitle.Admin }
            );
            modelBuilder.Entity<PageContent>().HasData(
                new PageContent
                { 
                    Id = 1, PageName = "About", 
                    TextContent = "Our application uses a highly efficient mathematical approach called Base62 Encoding." +
                    " When you submit a long URL, the system first securely saves it into our database" +
                    " and assigns it a unique numeric ID. We then take this standard base-10 numeric ID " +
                    "and convert it into a Base62 string. The Base62 alphabet consists of 62 characters:" +
                    " lowercase letters (a-z), uppercase letters (A-Z), and digits (0-9)." +
                    " This conversion guarantees that every generated short link is" +
                    " absolutely unique, collision-free, and as short as mathematically possible."
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
