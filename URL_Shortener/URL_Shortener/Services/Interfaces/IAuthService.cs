using URL_Shortener.DTOs;

namespace URL_Shortener.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> ValidateUserAsync(string userName, string password);

        Task LogoutAsync();
    }
}