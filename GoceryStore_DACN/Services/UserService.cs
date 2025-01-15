
using System.Security.Claims;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Helpers;
using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Respones;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoceryStore_DACN.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<ApplicationUser> userManager,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IEmailTemplateService emailTemplateService, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _emailTemplateService = emailTemplateService;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<LoginResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return new LoginResult
                {
                    Status = "Error",
                    Message = "User not found",
                    Token = "",
                    RefreshToken = ""
                };
            }

            var passwordVerificationResult = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash ?? string.Empty);
            if (!passwordVerificationResult)
            {
                return new LoginResult
                {
                    Status = "Error",
                    Message = "Invalid password",
                    Token = "",
                    RefreshToken = ""
                };
            }
            //get user roles 
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber ?? string.Empty),

            };
            //add role claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
                // Get role claims
                var roleClaims = await _roleManager.GetClaimsAsync(new IdentityRole(role));
                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }

            var token = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            return new LoginResult
            {
                Status = "Success",
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<RegistrationResult> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                if (registerRequest == null)
                {
                    return new RegistrationResult
                    {
                        Succeeded = false,
                        Error = "Registration request cannot be null"
                    };
                }

                var existingEmail = await _userManager.FindByEmailAsync(registerRequest.Email);
                if (existingEmail != null)
                {
                    return new RegistrationResult
                    {
                        Succeeded = false,
                        Error = "Email is already registered"
                    };
                }

                var existingUsername = await _userManager.FindByNameAsync(registerRequest.UserName);
                if (existingUsername != null)
                {
                    return new RegistrationResult
                    {
                        Succeeded = false,
                        Error = "Username is already taken"
                    };
                }


                var user = new ApplicationUser
                {
                    UserName = registerRequest.UserName,
                    Email = registerRequest.Email,
                    FullName = registerRequest.FullName,
                    PhoneNumber = registerRequest.PhoneNumber,
                    Address = registerRequest.Address,
                    EmailConfirmed = false
                };

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (result.Succeeded)
                {
                    //Add user to default role 
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.User ?? "User");
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = BuildConfirmationLink(user.Id, token);
                    Console.WriteLine(confirmationLink);
                    var emailTemplate =
                        _emailTemplateService.SendConfirmationEmailAsync(user.Email, user.UserName, confirmationLink);

                }
                return new RegistrationResult
                {
                    Succeeded = true,
                    User = user
                };
            }
            catch (Exception e)
            {
                return new RegistrationResult
                {
                    Succeeded = false,
                    Error = e.Message
                };
            }

        }

        public async Task<EmailConfirmationResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return EmailConfirmationResult.Failed(new[] { "User not found" });
            }

            if (user.EmailConfirmed)
            {
                return EmailConfirmationResult.Failed(new[] { "Email is already confirmed" });
            }

            try
            {
                var decodedToken = Uri.UnescapeDataString(token);
                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                Console.WriteLine("Email confirmed successfully");
                return EmailConfirmationResult.Success("Email confirmed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return EmailConfirmationResult.Failed(new[] { "An error occurred while confirming your email" });
            }
        }

        public async Task<ServiceResult> ForgotPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return ServiceResult.Success("If your email is registered, you will receive a password reset link.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = BuildResetLink(email, token);
                var emailTemplate = _emailTemplateService.SendConfirmationEmailAsync(email, user.UserName, resetLink);
                return ServiceResult.Success("Password reset link has been sent to your email.");
            }
            catch (Exception e)
            {
                return ServiceResult.Error("An error occurred while processing your request.");
            }
        }
        private string BuildResetLink(string email, string token)
        {
            var baseUrl = _configuration["AppSettings:BaseUrl"];
            return $"{baseUrl}/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
        }


        private string BuildConfirmationLink(string userId, string token)
        {
            var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "https://localhost:7139";
            Console.WriteLine(baseUrl);
            return $"{baseUrl}/confirm-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";
        }

        //APi get info 
        public async Task<UserInfo> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
        }
        //update user info 
        public async Task<UpdateUserInfo> UpdateUserInfoAsync(string userId, [FromBody] UpdateUserInfo updateUserInfo)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.FullName = updateUserInfo.FullName;
            user.PhoneNumber = updateUserInfo.PhoneNumber;
            await _userManager.UpdateAsync(user);
            return new UpdateUserInfo
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                // Validate input parameters
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Token) ||
                    string.IsNullOrEmpty(request.NewPassword))
                {
                    return ServiceResult.Error("Invalid request parameters");
                }

                // Find user by email
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return ServiceResult.Error("User not found");
                }

                // Decode token
                var decodedToken = Uri.UnescapeDataString(request.Token);

                // Verify token is valid
                var purpose = UserManager<ApplicationUser>.ResetPasswordTokenPurpose;
                var isValidToken = await _userManager.VerifyUserTokenAsync(user,
                    _userManager.Options.Tokens.PasswordResetTokenProvider,
                    purpose, decodedToken);

                if (!isValidToken)
                {
                    return ServiceResult.Error("Invalid or expired reset token");
                }

                // Reset the password using decoded token
                var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
                if (result.Succeeded)
                {
                    return ServiceResult.Success("Password reset successfully");
                }

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ServiceResult.Error($"Failed to reset password: {errors}");
            }
            catch (Exception ex)
            {
                return ServiceResult.Error($"An error occurred while processing your request: {ex.Message}");
            }
        }
        public async Task<ServiceResult> ValidateResetTokenAsync(string email, string token)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return ServiceResult.Error("Invalid request");
                }

                var purpose = UserManager<ApplicationUser>.ResetPasswordTokenPurpose;
                var isValid = await _userManager.VerifyUserTokenAsync(user,
                    _userManager.Options.Tokens.PasswordResetTokenProvider,
                    purpose, token);

                return isValid
                    ? ServiceResult.Success("Token is valid")
                    : ServiceResult.Error("Invalid or expired token");
            }
            catch (Exception)
            {
                return ServiceResult.Error("An error occurred while validating the token");
            }
        }
        public async Task<ServiceResult> ChangePasswordAsync([FromBody] RequestChangePassword request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return ServiceResult.Error("User not found");
            }
            var result = await _userManager.ChangePasswordAsync(user, request.NewPassword, request.ConfirmPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ServiceResult.Error($"Failed to change password: {errors}");
            }
            return ServiceResult.Success("Password changed successfully");
        }
        // Delete account
        public async Task<ServiceResult> DeleteAccountAsync(RequestDeleteAccount request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return ServiceResult.Error("User not found");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ServiceResult.Error($"Failed to delete account: {errors}");
            }
            return ServiceResult.Success("Account deleted successfully");
        }
    }
}
