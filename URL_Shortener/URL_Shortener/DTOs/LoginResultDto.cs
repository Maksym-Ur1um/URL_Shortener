namespace URL_Shortener.DTOs
{
    public class LoginResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public AuthResponseDto? ResponseDto { get; set; }
    }
}
