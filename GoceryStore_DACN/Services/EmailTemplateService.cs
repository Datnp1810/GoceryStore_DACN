using GoceryStore_DACN.Helpers;
using GoceryStore_DACN.Services.Interface;

namespace GoceryStore_DACN.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public EmailTemplateService(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }
        public Task SendConfirmationEmailAsync(string email, string userName, string confirmationLink)
        {
          
            var replacements = new Dictionary<string, string>
            {
                { "Username", userName },
                { "ConfirmationLink", confirmationLink },
                { "CompanyName", _configuration["CompanyInfo:Name"] },
                { "CompanyAddress", _configuration["CompanyInfo:Address"] }
            };
            //Log replacement 
            foreach (var replacement in replacements)
            {
                Console.WriteLine($"Key: {replacement.Key}, Value: {replacement.Value}");
            }
            string emailBody = EmailTemplateHelper.GetEmailTemplate("ConfirmationEmail", replacements);
            return _emailService.SendEmailAsync(email, "Confirm your email", emailBody);
        }

        public Task SendForgotPasswordEmailAsync(string email, string userName, string resetPasswordLink)
        {
            var replacements = new Dictionary<string, string>
            {
                { "UserName", userName },
                { "ResetLink", resetPasswordLink }
            };
            var emailBody = EmailTemplateHelper.GetEmailTemplate("ForgotPasswordEmail", replacements);
            return _emailService.SendEmailAsync(email, "Reset your password", emailBody);
        }
    }
}
