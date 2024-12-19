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
        public void SendEmailAsync(EmailDTO emailDTO)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("SenderEmail").Value));
            email.To.Add(MailboxAddress.Parse(emailDTO.EmailTo));
            email.Subject = emailDTO.EmailSubject;
            email.Body = new TextPart(TextFormat.Plain){Text = emailDTO.EmailBody };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("SenderEmail").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("SenderEmail").Value, _configuration.GetSection("SenderEmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
