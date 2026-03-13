using Microsoft.AspNetCore.Mvc;
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
            var result = await _authService.LoginAsync(loginRequestDto.UserName, loginRequestDto.Password);

            if (!result.IsSuccess)
            {
                return Unauthorized("Wrong Username or Password");
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            Response.Cookies.Append("jwt", result.Token, cookieOptions);

            return Ok(result.ResponseDto);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new { message = "Successfully logged out" });
        }
    }
}
