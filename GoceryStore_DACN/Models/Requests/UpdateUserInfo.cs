using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class UpdateUserInfo
{
  [Required]
  public string FullName { get; set; } = string.Empty;
  [Required]
  public string PhoneNumber { get; set; } = string.Empty;
}