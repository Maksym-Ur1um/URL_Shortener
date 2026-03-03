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

            base.OnModelCreating(modelBuilder);
        }
    }
}
