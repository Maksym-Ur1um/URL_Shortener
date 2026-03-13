using URL_Shortener.Data.Repository;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResultDto> LoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new LoginResultDto { IsSuccess = false };
            }

            var token = _tokenService.GenerateJwtToken(user);

            return new LoginResultDto
            {
                IsSuccess = true,
                Token = token,
                ResponseDto = new AuthResponseDto
                {
                    UserName = user.UserName,
                    Role = user.Role.ToString(),
                    UserId = user.Id
                }
            };
        }
    }
}
