using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Models.Respones;
using Models.Requests;
using Models.Respones;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IUserService
    {
        public Task<LoginResult> LoginAsync(LoginRequest loginRequest);
        public Task<RegistrationResult> RegisterAsync(RegisterRequest registerRequest);
        public Task<EmailConfirmationResult> ConfirmEmailAsync(string userId, string token);
        public Task<ServiceResult> ForgotPasswordAsync(string email);
        public Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request);
        public Task<ServiceResult> ValidateResetTokenAsync(string email, string token);
        public Task<UserInfo> GetUserInfoAsync(string userId);
        public Task<UpdateUserInfo> UpdateUserInfoAsync(string userId, UpdateUserInfo updateUserInfo);

    }
}
