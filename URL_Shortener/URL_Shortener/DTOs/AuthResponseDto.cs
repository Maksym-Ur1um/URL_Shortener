namespace URL_Shortener.DTOs
{
    public class AuthResponseDto
    {
        public required string UserName { get; set; }
        public required string Role { get; set; }
        public int UserId { get; set; }
    }
}