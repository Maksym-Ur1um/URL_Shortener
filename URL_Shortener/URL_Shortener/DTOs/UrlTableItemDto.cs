namespace URL_Shortener.DTOs
{
    public class UrlTableItemDto
    {
        public int Id { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public int? CreatorId { get; set; }
    }
}