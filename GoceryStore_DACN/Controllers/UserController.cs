using System.Security.Claims;
using GoceryStore_DACN.Models;
using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Requests;

namespace GoceryStore_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;
        private readonly IEmailTemplateService _emailTemplateService;
        public UserController(IOptions<JwtSettings> jwtSettings, IUserService userService, IEmailTemplateService emailTemplateService)
        {
            _jwtSettings = jwtSettings.Value;
            _userService = userService;
            _emailTemplateService = emailTemplateService;
        }
        [HttpPost("/login")]
        public async Task<IActionResult> LoginTask([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Invalid request"
                    });
                }
                var result = await _userService.LoginAsync(loginRequest);
                if (result == null)
                {
                    return BadRequest(result);
                }
                return Ok(new
                {
                    message = "Login successfully",
                    result
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
        [HttpPost("/register")]
        public async Task<IActionResult> RegisterTask([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _userService.RegisterAsync(registerRequest);
                if (!result.Succeeded)
                {
                    return BadRequest(new { error = result.Error });
                }

                return Ok(new
                {
                    message = "Registration successful. Please check your email to confirm your account.",
                    userId = result.User.Id
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }
        [HttpPost("/logout")]
        public async Task<IActionResult> LogoutTask()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }
        [HttpGet("/confirm-email")]
        public async Task<IActionResult> ConfirmEmailTask([FromQuery] string userId, [FromQuery] string token)
        {
            try
            {
                //get base url from appsettings.json\
                Console.WriteLine(userId, token);
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                {
                    return BadRequest(new
                    {
                        status = false,
                        error = "Invalid confirmation link"
                    });
                }
                var result = await _userService.ConfirmEmailAsync(userId, token);
                if (!result.Succeeded)
                {
                    return BadRequest(new { errors = result.Errors });
                }
                return Ok(new
                {
                    message = "Email confirmed successfully"
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

        [HttpGet("/get-user-info")]
        [Authorize]
        public async Task<IActionResult> GetUserInfoTask()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "User not found" });
            }
            var result = await _userService.GetUserInfoAsync(userId);
            return Ok(result);
        }
        [HttpPut("/update-user-info")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfoTask([FromBody] UpdateUserInfo updateUserInfo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "User not found" });
            }
            var result = await _userService.UpdateUserInfoAsync(userId, updateUserInfo);
            return Ok(result);
        }
        [HttpPost("/change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordTask([FromBody] RequestChangePassword request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "User not found" });
            }
            var result = await _userService.ChangePasswordAsync(request);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(new { message = "Password changed successfully" });
        }
        // [HttpPost("/refresh-token")]
        // public async Task<IActionResult> RefreshTokenTask()
        // {
        //     return Ok();
        // }
        [HttpDelete("/delete-acccount")]
        [Authorize]
        public async Task<IActionResult> DeleteAccountTask()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "User not found" });
            }
            var result = await _userService.DeleteAccountAsync(userId);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(new { message = "Account deleted successfully" });
        }
        [HttpPost("/resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmailTask([FromBody] ResendConfirmationEmailRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Invalid request"
                    });
                }

                var result = await _userService.ResendConfirmationEmailAsync(request.Email);
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

