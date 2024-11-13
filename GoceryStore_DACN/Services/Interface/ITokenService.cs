using System.Security.Claims;

namespace GoceryStore_DACN.Services.Interface
{
    public interface ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
    }
}
