using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using URL_Shortener.Data.Repository;
using URL_Shortener.DTOs;
using URL_Shortener.Models;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> ValidateUserAsync(string userName, string password)
        {

            var user = await _userRepository.GetByUserNameAsync(userName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new LoginResponseDto { IsSuccess = false };
            }

            var principal = CreatePrincipal(user);

            return new LoginResponseDto
            {
                IsSuccess = true,
                Principal = principal,
                ResponseDto = new AuthResponseDto
                {
                    UserName = user.UserName,
                    Role = user.Role.ToString(),
                    UserId = user.Id
                }
            };
        }

        private ClaimsPrincipal CreatePrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
