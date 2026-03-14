using System.Security.Claims;

namespace URL_Shortener.DTOs
{
    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public ClaimsPrincipal Principal { get; set; }
        public AuthResponseDto? ResponseDto { get; set; }
    }
}
