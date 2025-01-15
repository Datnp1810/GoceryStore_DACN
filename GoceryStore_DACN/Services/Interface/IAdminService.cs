using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Models.Responses;

namespace GoceryStore_DACN.Services.Interface
{
  public interface IAdminService
  {
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    Task<ServiceResult> DeleteUser(string userId);
    Task<ServiceResult> AddUserToRole(string userId, string roleName);
    Task<ServiceResult> RemoveUserFromRole(string userId, string roleName);
    Task<ServiceResult> GetUserDetails(string userId);
    Task<IEnumerable<OrderManagementResponse>> GetAllOrders();
    Task<ServiceResult> UpdateOrderStatus(int orderId, int statusId);
  }
}