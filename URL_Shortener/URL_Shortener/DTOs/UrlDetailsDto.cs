namespace URL_Shortener.DTOs
{
    public class UrlDetailsDto
    {
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatorName { get; set; }
    }
}