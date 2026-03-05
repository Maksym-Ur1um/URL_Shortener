using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ITokenService _tokenService;

        
        public AuthController(AppDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == loginRequestDto.UserName);

            if (user == null)
            {
                return Unauthorized("Wrong Username or Password");
            }
            if(BCrypt.Net.BCrypt.Verify(loginRequestDto.Password,  user.PasswordHash))
            {
                var token = _tokenService.GenerateJwtToken(user);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddHours(1)   
                };
                
                Response.Cookies.Append("jwt", token, cookieOptions);
                var authResponse = new AuthResponseDto 
                { UserName = user.UserName, Role = user.Role.ToString(), UserId = user.Id };
                return Ok(authResponse);
            }

            return Unauthorized("Wrong Username or Password");
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
