using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 character")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}