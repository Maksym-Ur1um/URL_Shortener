using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTOs;
using URL_Shortener.Models;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly IAntiforgery _antiforgery;

        public AuthController(UserManager<User> userManager, IAuthService authService, IAntiforgery antiforgery)
        {
            _userManager = userManager;
            _authService = authService;
            _antiforgery = antiforgery;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new User
            {
                UserName = registerRequestDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                return Ok(new { message = "Registration successful" });
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
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