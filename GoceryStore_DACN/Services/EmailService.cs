using System.Net;
using GoceryStore_DACN.Models;
using GoceryStore_DACN.Services.Interface;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace GoceryStore_DACN.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                // Tạo message
                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                mail.To.Add(new MailAddress(email));

                //Cấu hình SMTP Client 
                using var smtp = new SmtpClient();
                {
                    smtp.Host = _emailSettings.Server;
                    smtp.Port = int.Parse(_emailSettings.Port);
                    smtp.EnableSsl = bool.Parse(_emailSettings.EnableSSL);

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(
                        _emailSettings.SenderEmail,         
                        _emailSettings.AppPassword                
                    );

                }
                //Send mail
                try
                {
                    await smtp.SendMailAsync(mail);
                    Console.WriteLine($"Email sent successfully to {email}");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"SMTP error occurred: {ex.Message}");
                    Console.WriteLine($"Status code: {ex.StatusCode}");
                    throw; // Re-throw để caller handle
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
