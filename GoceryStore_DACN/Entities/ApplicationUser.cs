using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GoceryStore_DACN.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Address { get; set; } = string.Empty;
    }
}
