using URL_Shortener.Data.Repository;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class UrlManagementService : IUrlManagementService
    {
        private readonly IUrlRepository _repository;
        private readonly IUrlBuilderService _urlBuilder;

        public UrlManagementService(IUrlRepository repository, IUrlBuilderService urlBuilder)
        {
            _repository = repository;
            _urlBuilder = urlBuilder;
        }

        public async Task<string?> GetOriginalUrlAsync(string shortCode)
        {
            var urlEntity = await _repository.GetByShortUrlAsync(shortCode);
            return urlEntity?.OriginalUrl;
        }

        public async Task<IEnumerable<UrlTableItemDto>> GetAllUrlsAsync()
        {
            var urls = await _repository.GetAllUrlsAsync();

            return urls.Select(u => new UrlTableItemDto
            {
                Id = u.Id,
                OriginalUrl = u.OriginalUrl,
                ShortUrl = _urlBuilder.BuildFullShortUrl(u.ShortUrl),
                CreatorId = u.UserId
            }).ToList();
        }

        public async Task<UrlDetailsDto?> GetUrlDetailsAsync(int id)
        {
            var urlEntity = await _repository.GetByIdAsync(id);
            if (urlEntity == null) return null;

            return new UrlDetailsDto
            {
                OriginalUrl = urlEntity.OriginalUrl,
                ShortUrl = _urlBuilder.BuildFullShortUrl(urlEntity.ShortUrl),
                CreatedAt = urlEntity.CreatedAt,
                CreatorName = urlEntity.User?.UserName
            };
        }

        public async Task DeleteUrlAsync(int id, int currentUserId, bool isAdmin)
        {
            var urlEntity = await _repository.GetByIdAsync(id);

            if (urlEntity == null)
            {
                throw new KeyNotFoundException("URL not found.");
            }

            if (urlEntity.UserId != currentUserId && !isAdmin)
            {
                throw new UnauthorizedAccessException("You don't have permission to delete this URL.");
            }

            _repository.Delete(urlEntity);
            await _repository.SaveChangesAsync();
        }
    }
}
