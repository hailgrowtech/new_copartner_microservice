namespace EmailService.Logic
{
    public interface IEmailBusinessProcessor
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
