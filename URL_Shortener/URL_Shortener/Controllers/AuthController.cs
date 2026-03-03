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
                var authResponse = new AuthResponseDto 
                { UserName = user.UserName, Token = token, Role = user.Role.ToString() };
                return Ok(authResponse);
            }

            return Unauthorized("Wrong Username or Password");
        }
    }
}
