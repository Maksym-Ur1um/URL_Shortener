namespace URL_Shortener.Services.Interfaces
{
    public interface IUrlBuilderService
    {
        public string BuildFullShortUrl(string shortCode);
    }
}
