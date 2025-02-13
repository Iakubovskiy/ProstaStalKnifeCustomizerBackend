using WorkshopBackend.DTO;

namespace WorkshopBackend.Interfaces
{
    public interface ICustomEmailSender
    {
        public Task SendEmailAsync(EmailDTO data);
    }
}
