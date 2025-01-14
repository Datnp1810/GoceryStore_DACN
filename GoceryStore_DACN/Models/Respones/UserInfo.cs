using System.ComponentModel.DataAnnotations;

namespace Models.Respones;
public class UserInfo
{
  [Required]
  public string Id { get; set; }
  [Required]
  public string Email { get; set; }
  [Required]
  public string FullName { get; set; }
  [Required]
  public string PhoneNumber { get; set; }
  [Required]
  public string Address { get; set; }
}
