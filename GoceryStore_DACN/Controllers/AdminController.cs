using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GoceryStore_DACN.Helpers;

namespace GoceryStore_DACN.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  // [Authorize(Roles = "Admin")]
  public class AdminController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminController> _logger;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IAdminService adminService,
        ILogger<AdminController> logger)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _adminService = adminService;
      _logger = logger;
    }

    [HttpGet("users")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> GetAllUsers()
    {
      try
      {
        var users = await _adminService.GetAllUsersAsync();
        return Ok(new
        {
          status = true,
          message = "Lấy danh sách người dùng thành công",
          results = users
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error getting users list");
        return BadRequest(new
        {
          status = false,
          message = "Lỗi khi lấy danh sách người dùng"
        });
      }
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
      try
      {
        var result = await _adminService.DeleteUser(userId);
        return Ok(result);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error deleting user");
        return BadRequest(new
        {
          status = false,
          message = "Lỗi khi xóa người dùng"
        });
      }
    }
    //AddUserToRole
    [HttpPost("users/{userId}/roles")]
    public async Task<IActionResult> AddUserToRole(string userId, string roleName)
    {
      try
      {
        var result = await _adminService.AddUserToRole(userId, roleName);
        return Ok(result);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error adding user to role");
        return BadRequest(new
        {
          status = false,
          message = "Lỗi khi thêm role cho người dùng"
        });
      }
    }
    [HttpDelete("users/{userId}/roles/{roleName}")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
    {
      try
      {
        var result = await _adminService.RemoveUserFromRole(userId, roleName);
        if (!result.Succeeded)
        {
          return BadRequest(new { status = false, message = result.Message });
        }
        return Ok(new { status = true, message = result.Message });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error removing user from role");
        return BadRequest(new { status = false, message = "Lỗi khi xóa role người dùng" });
      }
    }

    [HttpGet("users/{userId}")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> GetUserDetails(string userId)
    {
      try
      {
        var result = await _adminService.GetUserDetails(userId);
        if (!result.Succeeded)
        {
          return BadRequest(new { status = false, message = result.Message });
        }
        return Ok(new
        {
          status = true,
          message = result.Message,
          user = result.Data
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error getting user details");
        return BadRequest(new { status = false, message = "Lỗi khi lấy thông tin người dùng" });
      }
    }
    [HttpGet("orders")]
    public async Task<IActionResult> GetAllOrders()
    {
      try
      {
        var orders = await _adminService.GetAllOrders();
        return Ok(new { status = true, data = orders });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error getting orders");
        return BadRequest(new { status = false, message = "Lỗi khi lấy danh sách đơn hàng" });
      }
    }

    [HttpPut("orders/{orderId}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] int statusId)
    {
      try
      {
        var result = await _adminService.UpdateOrderStatus(orderId, statusId);
        return result.Succeeded
            ? Ok(new { status = true, message = result.Message })
            : BadRequest(new { status = false, message = result.Message });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error updating order status");
        return BadRequest(new { status = false, message = "Lỗi khi cập nhật trạng thái đơn hàng" });
      }
    }
  }
}