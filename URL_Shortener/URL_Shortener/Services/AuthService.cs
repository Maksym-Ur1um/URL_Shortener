using Microsoft.AspNetCore.Identity;
using URL_Shortener.DTOs;
using URL_Shortener.Models;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
           

        public async Task<LoginResponseDto> ValidateUserAsync(string userName, string password)
        {

            var isAuthenticated = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: true, lockoutOnFailure: false);
            if (isAuthenticated.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);

                var userRole = await _userManager.GetRolesAsync(user);
                return new LoginResponseDto
                {
                    IsSuccess = true,
                    ResponseDto = new AuthResponseDto
                    {
                        UserName = userName,
                        Role = userRole.FirstOrDefault(),
                        UserId = user.Id
                    }
                };
            }
            return new LoginResponseDto
            {
                IsSuccess = false,
            };

        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
