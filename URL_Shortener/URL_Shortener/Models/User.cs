
namespace URL_Shortener.Models
{

    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public RoleTitle Role { get; set; }
        public List<ShortenedUrl> Urls { get; set; } = new List<ShortenedUrl>();
        public enum RoleTitle
        {
            Admin,
            OrdinaryUser
        }
    }
}
