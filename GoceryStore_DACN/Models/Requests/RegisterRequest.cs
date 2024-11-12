using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        
    }
}
