using WorkshopBackend.DTO;

namespace WorkshopBackend.Interfaces
{
    public interface ICustomEmailSender
    {
        void SendEmailAsync(EmailDTO data);
    }
}
