namespace URL_Shortener.DTOs
{
    public class UrlResponseDto
    {
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
    }
}
