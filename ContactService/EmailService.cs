using ContactUsService.Dto;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    public async Task SendEmail(SendEmailCommand command)
    {
        var mailMessage = new MailMessage("your-email@example.com", command.ToEmail)
        {
            Subject = command.Subject,
            Body = command.Body
        };

        using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("your-email@example.com", "your-password");
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}


