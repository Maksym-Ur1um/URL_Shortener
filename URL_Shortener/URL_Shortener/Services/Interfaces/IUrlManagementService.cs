using URL_Shortener.DTOs;

namespace URL_Shortener.Services.Interfaces
{
    public interface IUrlManagementService
    {
        Task<string?> GetOriginalUrlAsync(string shortCode);
        Task<IEnumerable<UrlTableItemDto>> GetAllUrlsAsync();
        Task<UrlDetailsDto?> GetUrlDetailsAsync(int id);
        Task DeleteUrlAsync(int id, int currentUserId, bool isAdmin);
    }
}
