using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Models.Requests
{
  public class ResendConfirmationEmailRequest
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
  }
}