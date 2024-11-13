namespace GoceryStore_DACN.Services.Interface
{
    public interface IEmailTemplateService
    {
        public Task SendConfirmationEmailAsync(string email, string userName, string confirmationLink);
        public Task SendForgotPasswordEmailAsync(string email, string userName, string resetPasswordLink);
    }
}
