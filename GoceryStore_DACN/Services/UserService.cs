﻿using System;
using System.Security.Claims;
using System.Text;
using Azure.Core;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models;
using GoceryStore_DACN.Models.Requests;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace GoceryStore_DACN.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IEmailTemplateService _emailTemplateService; 
        private readonly IConfiguration _configuration;
        public UserService(UserManager<ApplicationUser> userManager, 
            IPasswordHasher passwordHasher, 
            ITokenService tokenService, 
            IEmailTemplateService emailTemplateService, IConfiguration configuration)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _emailTemplateService = emailTemplateService;
            _configuration = configuration;
        }
        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return new LoginResult
                {
                   Status = "Error",
                   Token = null,
                   RefreshToken = null
                };
            }

            var passwordVerificationResult = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!passwordVerificationResult)
            {
                return new LoginResult
                {
                    Status = "Error",
                    Token = null,
                    RefreshToken = null
                };
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                // Add other claims as needed
            };

            var token = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            return new LoginResult
            {
                Status = "Success",
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<RegistrationResult> Register(RegisterRequest registerRequest)
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
                Console.WriteLine(e);
                throw;
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

        public Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> ValidateResetTokenAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        private string BuildConfirmationLink(string userId, string token)
        {
            var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "https://localhost:7139";
            Console.WriteLine(baseUrl);
            return $"{baseUrl}/confirm-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";
        }
        
    }
}
