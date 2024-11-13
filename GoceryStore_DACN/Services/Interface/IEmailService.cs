namespace GoceryStore_DACN.Services.Interface
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
