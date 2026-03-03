using URL_Shortener.Models;

namespace URL_Shortener.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
    }
}
