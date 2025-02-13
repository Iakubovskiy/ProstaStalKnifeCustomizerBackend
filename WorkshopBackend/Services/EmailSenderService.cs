using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using WorkshopBackend.DTO;
using WorkshopBackend.Interfaces;

namespace WorkshopBackend.Services
{
    public class EmailSenderService : ICustomEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(EmailDTO emailDto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("SenderEmail")));
            email.To.Add(MailboxAddress.Parse(emailDto.EmailTo));
            email.Subject = emailDto.EmailSubject;
            email.Body = new TextPart(TextFormat.Plain){Text = emailDto.EmailBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetValue<string>("EmailHost"), 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration.GetValue<string>("SenderEmail"), _configuration.GetValue<string>("SenderEmailPassword"));
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
