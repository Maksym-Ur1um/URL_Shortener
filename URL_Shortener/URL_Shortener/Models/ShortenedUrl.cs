
namespace URL_Shortener.Models
{
    public class ShortenedUrl
    {
        public int Id { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
