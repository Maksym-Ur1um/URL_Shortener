using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var result = await _authService.ValidateUserAsync(loginRequestDto.UserName, loginRequestDto.Password);

            if (!result.IsSuccess)
            {
                return Unauthorized("Wrong Username or Password");
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, result.Principal!);

            return Ok(result.ResponseDto);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Ok(new { message = "Successfully logged out" });
        }
    }
}
