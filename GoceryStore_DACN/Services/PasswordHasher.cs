using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace GoceryStore_DACN.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> _passwordHasher;

        public PasswordHasher()
        {
            _passwordHasher = new PasswordHasher<object>();
        }
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
