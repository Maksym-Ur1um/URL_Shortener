namespace URL_Shortener.DTOs
{
    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public AuthResponseDto? ResponseDto { get; set; }
    }
}