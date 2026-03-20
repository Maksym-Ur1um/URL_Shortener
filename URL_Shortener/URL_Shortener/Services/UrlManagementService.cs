using URL_Shortener.Data.Repository;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class UrlManagementService : IUrlManagementService
    {
        private readonly IUrlRepository _UrlRepository;

        public UrlManagementService(IUrlRepository repository)
        {
            _UrlRepository = repository;
        }

        public async Task<string?> GetOriginalUrlAsync(string shortCode)
        {
            var urlEntity = await _UrlRepository.GetByShortUrlAsync(shortCode);
            return urlEntity?.OriginalUrl;
        }

        public async Task<IEnumerable<UrlTableItemDto>> GetAllUrlsAsync()
        {
            var urls = await _UrlRepository.GetAllUrlsAsync();

            return urls.Select(u => new UrlTableItemDto
            {
                Id = u.Id,
                OriginalUrl = u.OriginalUrl,
                ShortUrl = u.ShortUrl,
                CreatorId = u.UserId
            }).ToList();
        }

        public async Task<UrlDetailsDto?> GetUrlDetailsAsync(int id)
        {
            var urlEntity = await _UrlRepository.GetByIdAsync(id);
            if (urlEntity == null) return null;

            return new UrlDetailsDto
            {
                OriginalUrl = urlEntity.OriginalUrl,
                ShortUrl = urlEntity.ShortUrl,
                CreatedAt = urlEntity.CreatedAt,
                CreatorName = urlEntity.User?.UserName
            };
        }

        public async Task DeleteUrlAsync(int id, int currentUserId, bool isAdmin)
        {
            var urlEntity = await _UrlRepository.GetByIdAsync(id);

            if (urlEntity == null)
            {
                throw new KeyNotFoundException("URL not found.");
            }

            if (urlEntity.UserId != currentUserId && !isAdmin)
            {
                throw new UnauthorizedAccessException("You don't have permission to delete this URL.");
            }

            _UrlRepository.Delete(urlEntity);
            await _UrlRepository.SaveChangesAsync();
        }
    }
}