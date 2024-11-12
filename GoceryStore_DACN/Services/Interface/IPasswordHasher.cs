using GoceryStore_DACN.Entities;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
