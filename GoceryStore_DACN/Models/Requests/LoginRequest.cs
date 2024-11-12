using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Models.Requests
{
    public class LoginRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [MinLength(6), MaxLength(50)]
        public string Password { get; set; } = string.Empty; 
    }
}
