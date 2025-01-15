namespace GoceryStore_DACN.Models.Responses
{
  public class UserResponse
  {
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> Roles { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}