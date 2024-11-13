using GoceryStore_DACN.Entities;

namespace GoceryStore_DACN.Models.Respones
{
    public class RegistrationResult
    {
        public bool Succeeded { get; set; }
        public ApplicationUser User { get; set; }
        public string Error { get; set; }
    }
}
