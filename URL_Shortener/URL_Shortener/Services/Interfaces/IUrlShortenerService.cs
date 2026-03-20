using URL_Shortener.DTOs;

namespace URL_Shortener.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        Task<UrlResponseDto> ShortenLinkAsync(string originalUrl, int userId);
    }
}