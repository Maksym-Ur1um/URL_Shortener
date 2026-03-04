using URL_Shortener.Models;

namespace URL_Shortener.Repository
{
    public interface IUrlRepository
    {
        Task<ShortenedUrl?> GetByIdAsync(int id);
        Task<ShortenedUrl?> GetByOriginalUrlAsync(string originalUrl);
        Task<ShortenedUrl?> GetByShortUrlAsync(string shortUrl);
        Task<IEnumerable<ShortenedUrl>> GetAllUrlsAsync();
        void Add(ShortenedUrl urlEntity);
        void Delete(ShortenedUrl urlEntity);
        Task SaveChangesAsync();
    }
}
