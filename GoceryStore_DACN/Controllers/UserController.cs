using System.Security.Claims;
using GoceryStore_DACN.Models;
using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Services.Interface;
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
            throw new NotImplementedException();
        }
        [HttpGet("/confirm-email")]
        public async Task<IActionResult> ConfirmEmailTask([FromQuery] string userId, [FromQuery] string token)
        {
            try
            {
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


    }
}
