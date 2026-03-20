using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _dbContext;

        public UrlRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShortenedUrl?> GetByIdAsync(int id)
        {
            return await _dbContext.ShortenedUrls
                .Include(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ShortenedUrl?> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _dbContext.ShortenedUrls
                .FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
        }

        public async Task<ShortenedUrl?> GetByShortUrlAsync(string shortUrl)
        {
            return await _dbContext.ShortenedUrls
                .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
        }

        public async Task<IEnumerable<ShortenedUrl>> GetAllUrlsAsync()
        {
            return await _dbContext.ShortenedUrls.ToListAsync();
        }

        public void Add(ShortenedUrl urlEntity)
        {
            _dbContext.ShortenedUrls.Add(urlEntity);
        }

        public void Delete(ShortenedUrl urlEntity)
        {
            _dbContext.ShortenedUrls.Remove(urlEntity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}