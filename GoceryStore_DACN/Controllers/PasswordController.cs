using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoceryStore_DACN.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PasswordController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly ILogger<PasswordController> _logger;


    public PasswordController(IUserService userService, ILogger<PasswordController> logger)
    {
      _userService = userService;
      _logger = logger;
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
      try
      {
        var result = await _userService.ForgotPasswordAsync(request.Email);
        return Ok(new
        {
          status = true,
          message = result.Message,
        });
      }
      catch (Exception e)
      {
        return BadRequest(new
        {
          status = false,
          message = e.Message,
        });
      }
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        // Log ra các lỗi cụ thể
        _logger.LogError($"Reset password validation failed: {string.Join(", ", errors)}");
        return BadRequest(new
        {
          status = false,
          message = "Invalid request",
          errors = errors
        });
      }

      try
      {
        var result = await _userService.ResetPasswordAsync(request);
        if (result.Succeeded)
        {
          return Ok(new
          {
            status = true,
            message = result.Message
          });
        }

        // Log lỗi từ service
        _logger.LogError($"Reset password failed: {result}");
        return BadRequest(new
        {
          status = false,
          message = result.Message
        });
      }
      catch (Exception e)
      {
        _logger.LogError($"Reset password exception: {e.Message}");
        return BadRequest(new
        {
          status = false,
          message = e.Message
        });
      }
    }
    [HttpPost("validate-reset-token")]
    public async Task<IActionResult> ValidateResetToken([FromQuery] string email, [FromQuery] string token)
    {
      try
      {
        var result = await _userService.ValidateResetTokenAsync(email, token);
        if (result.Succeeded)
        {
          return Ok(new
          {
            status = true,
            message = result.Message
          });
        }

        return BadRequest(new
        {
          status = false,
          message = result.Message
        });
      }
      catch (Exception e)
      {
        return BadRequest(new
        {
          status = false,
          message = e.Message
        });
      }
    }
  }
}