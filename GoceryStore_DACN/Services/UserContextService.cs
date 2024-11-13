using System.Security.Claims;
using GoceryStore_DACN.Services.Interface;

namespace GoceryStore_DACN.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserId()
        {
           var user = _httpContextAccessor.HttpContext?.User;
           if (user == null || !user.Identity.IsAuthenticated)
           {
               return null;
           }
           var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           if (!string.IsNullOrEmpty(userId))
           {
               return userId; 
           }
           userId = user.FindFirst("sub")?.Value;
           if (!string.IsNullOrEmpty(userId))
               return userId;

           throw new Exception("User ID not found in token claims");
        }
        public string GetCurrentUserName()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            return user.Identity.Name;
        }

        public List<string> GetCurrentUserRoles()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            return user.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
        }

        public bool IsAuthenticated()
        {
           return _httpContextAccessor.HttpContext?.User?.Identity.IsAuthenticated ?? false;
        }
    }
}
