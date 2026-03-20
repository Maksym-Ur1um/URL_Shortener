using Microsoft.AspNetCore.Antiforgery;
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
        private readonly IAntiforgery _antiforgery;

        public AuthController(IAuthService authService, IAntiforgery antiforgery)
        {
            _authService = authService;
            _antiforgery = antiforgery;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var result = await _authService.ValidateUserAsync(loginRequestDto.UserName, loginRequestDto.Password);

            if (!result.IsSuccess)
            {
                return Unauthorized("Wrong Username or Password");
            }

            return Ok(result.ResponseDto);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();

            return Ok(new { message = "Successfully logged out" });
        }

        [HttpGet("csrf-token")]
        public IActionResult Csrf_Token()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);

            Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Lax
            });
            return NoContent();
        }
    }
}