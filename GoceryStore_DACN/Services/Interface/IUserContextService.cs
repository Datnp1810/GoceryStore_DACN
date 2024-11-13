namespace GoceryStore_DACN.Services.Interface
{
    public interface IUserContextService
    {
        string GetCurrentUserId();
        string GetCurrentUserName();
        List<string> GetCurrentUserRoles();
        bool IsAuthenticated();
    }
}
