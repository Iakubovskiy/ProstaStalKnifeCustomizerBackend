namespace Application
{
    public interface ICustomEmailSender
    {
        public Task SendEmailAsync(EmailDTO data);
    }
}
