using AutoMapper;
using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Helpers;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Models.Responses;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Services
{
  public class AdminService : IAdminService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;



    public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IMapper mapper)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _context = context;
    }
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
      var users = await _userManager.Users.ToListAsync(); // get all user 
      var userResponses = new List<UserResponse>();
      foreach (var user in users)
      {
        var roles = await _userManager.GetRolesAsync(user);
        var userResponse = new UserResponse
        {
          Id = user.Id,
          UserName = user.UserName,
          Email = user.Email,
          EmailConfirmed = user.EmailConfirmed,
          PhoneNumber = user.PhoneNumber,
          Roles = roles.ToList()
        };
        userResponses.Add(userResponse);
      }
      return userResponses;
    }
    public async Task<ServiceResult> DeleteUser(string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return ServiceResult.Error("Người dùng không tồn tại");
      }
      var isAdmin = await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin);
      if (isAdmin)
      {
        return ServiceResult.Error("Không thể xóa tài khoản Admin");
      }
      var result = await _userManager.DeleteAsync(user);
      if (!result.Succeeded)
      {
        return ServiceResult.Error("Xóa người dùng thất bại");
      }
      return ServiceResult.Success("Xóa người dùng thành công");
    }
    public async Task<ServiceResult> AddUserToRole(string userId, string roleName)
    {
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return ServiceResult.Error("Người dùng không tồn tại");
      }

      if (!await _roleManager.RoleExistsAsync(roleName))
      {
        return ServiceResult.Error("Role không tồn tại");
      }

      if (await _userManager.IsInRoleAsync(user, roleName))
      {
        return ServiceResult.Error("Người dùng đã có role này");
      }

      var result = await _userManager.AddToRoleAsync(user, roleName);
      return result.Succeeded
          ? ServiceResult.Success("Thêm role thành công")
          : ServiceResult.Error("Thêm role thất bại");
    }
    public async Task<ServiceResult> RemoveUserFromRole(string userId, string roleName)
    {
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return ServiceResult.Error("Người dùng không tồn tại");
      }

      if (!await _roleManager.RoleExistsAsync(roleName))
      {
        return ServiceResult.Error("Role không tồn tại");
      }

      if (!await _userManager.IsInRoleAsync(user, roleName))
      {
        return ServiceResult.Error("Người dùng không có role này");
      }

      // Không cho phép xóa role Admin của tài khoản Admin gốc
      if (roleName == ApplicationRoles.Admin && user.Email == "admin@gmail.com")
      {
        return ServiceResult.Error("Không thể xóa role Admin của tài khoản Admin gốc");
      }

      var result = await _userManager.RemoveFromRoleAsync(user, roleName);
      return result.Succeeded
          ? ServiceResult.Success("Xóa role thành công")
          : ServiceResult.Error("Xóa role thất bại");
    }

    public async Task<ServiceResult> GetUserDetails(string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return ServiceResult.Error("Người dùng không tồn tại");
      }

      var roles = await _userManager.GetRolesAsync(user);
      var userDetails = new UserResponse
      {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        EmailConfirmed = user.EmailConfirmed,
        PhoneNumber = user.PhoneNumber,
        Roles = roles.ToList()
      };

      return ServiceResult.Success("Lấy thông tin người dùng thành công", userDetails);
    }
    public async Task<IEnumerable<OrderManagementResponse>> GetAllOrders()
    {
      var orders = await _context.HoaDons
          .Include(h => h.ApplicationUser)
          .Include(h => h.TinhTrang)
          .Include(h => h.CTHoaDons)
              .ThenInclude(ct => ct.ThucPham)
          .OrderByDescending(h => h.NgayLap)
          .Select(h => new OrderManagementResponse
          {
            OrderId = h.MAHD,
            UserId = h.ApplicationUser.Id,
            CustomerName = h.ApplicationUser.UserName,
            OrderDate = h.NgayLap,
            StatusName = h.TinhTrang.TenTinhTrang,
            StatusId = h.TinhTrang.ID_TT,
            TotalAmount = (decimal)h.TongTien,
            Items = h.CTHoaDons.Select(ct => new OrderItemResponse
            {
              ProductId = ct.ID_ThucPham,
              ProductName = ct.ThucPham.TenThucPham,
              Quantity = (int)ct.SoLuong,
              Price = (decimal)ct.DonGia
            }).ToList()
          })
          .ToListAsync();

      return orders;
    }



    public async Task<ServiceResult> UpdateOrderStatus(int orderId, int statusId)
    {
      var order = await _context.HoaDons.FindAsync(orderId);
      if (order == null)
      {
        return ServiceResult.Error("Đơn hàng không tồn tại");
      }

      var status = await _context.TinhTrangs.FindAsync(statusId);
      if (status == null)
      {
        return ServiceResult.Error("Trạng thái không hợp lệ");
      }

      order.ID_TT = statusId;
      await _context.SaveChangesAsync();

      return ServiceResult.Success("Cập nhật trạng thái đơn hàng thành công");
    }
  }
}