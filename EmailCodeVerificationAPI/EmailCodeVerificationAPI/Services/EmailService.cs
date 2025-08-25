using EmailCodeVerificationAPI.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EmailCodeVerificationAPI.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(string toEmail, string code)
        {
            var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.AppPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail),
                Subject = "Your Verification Code",
                Body = $"Your verification code is: {code}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
        }
    }
}

