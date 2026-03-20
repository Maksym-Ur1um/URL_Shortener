using Microsoft.AspNetCore.Identity;

namespace URL_Shortener.Models
{
    public class User : IdentityUser<int>
    {
        public List<ShortenedUrl> Urls { get; set; } = new List<ShortenedUrl>();
    }
}