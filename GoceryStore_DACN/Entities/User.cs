using Microsoft.AspNetCore.Identity;

namespace GoceryStore_DACN.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get;set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
