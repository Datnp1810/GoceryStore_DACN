using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Models.Requests
{
    public class ForgotPasswordRequest
    {
        [Required][EmailAddress] public string Email { get; set; } = string.Empty;
    }
}
