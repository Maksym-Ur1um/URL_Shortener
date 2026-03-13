using URL_Shortener.DTOs;

namespace URL_Shortener.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResultDto> LoginAsync(string userName, string password);
    }
}
