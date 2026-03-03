
namespace URL_Shortener.Models
{
    public class PageContent
    {
        public int Id { get; set; }
        public required string PageName { get; set; }
        public required string TextContent { get; set; }
    }
}
