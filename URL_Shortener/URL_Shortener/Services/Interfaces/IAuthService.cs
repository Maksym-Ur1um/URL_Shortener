using URL_Shortener.DTOs;

namespace URL_Shortener.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> ValidateUserAsync(string userName, string password);
    }
}
