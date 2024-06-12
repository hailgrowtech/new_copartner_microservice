using MimeKit;
using System.Net;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CommonLibrary.CommonDTOs;
using CommonLibrary;

namespace Publication.Factory;

public class eMailFactory
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<eMailFactory> _logger;
    public eMailFactory(IConfiguration configuration, ILogger<eMailFactory> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<HttpStatusCode> PostEmailAsync(string[] to, string[] cc, string subject, string content, string emailType)
    {
        try
        {
            var message = new Message(to, cc, subject, content);
            var statusCode = await SendEmailAsync(message, emailType);
            return statusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error posting email");
            return HttpStatusCode.InternalServerError;
        }
    }

    public async Task<HttpStatusCode> SendEmailAsync(Message message, string emailType)
    {
        var emailSettings = GetEmailSettings(emailType);

        var emailMessage = CreateEmailMessage(message, emailSettings);

        try
        {
            await SendAsync(emailMessage, emailSettings);
            return HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email");
            return HttpStatusCode.InternalServerError;
        }
    }
    private EmailSettings GetEmailSettings(string emailType)
    {
        var emailConfiguration = ConfigHelper.GetEmailSettings(emailType);
        // var emailConfiguration = emailSettings.GetSection($"EmailConfigurations:{emailType}");
        var emailSettings = new EmailSettings
        {
            Server = emailConfiguration.SmtpServer,
            Port = Convert.ToInt32(emailConfiguration.Port),
            Username = emailConfiguration.Username,
            Password = emailConfiguration.Password,
            From = emailConfiguration.From,
            FromDisplayName = emailConfiguration.FromDisplayName
        };
        return emailSettings;
    }

    private MimeMessage CreateEmailMessage(Message message, EmailSettings _emailSettings)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailSettings.FromDisplayName, _emailSettings.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Cc.AddRange(message.Cc);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage, EmailSettings _emailSettings)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                //Decrypt password
                string Password = EncryptionHelper.DecryptString(_emailSettings.Password);

                _logger.LogInformation("Connecting to SMTP server...");
                await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                _logger.LogInformation("Authenticating...");
                await client.AuthenticateAsync(_emailSettings.Username, Password);

                _logger.LogInformation("Sending email...");
                await client.SendAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMTP error");
                throw;
            }
            finally
            {
                _logger.LogInformation("Disconnecting from SMTP server...");
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}

public class Message
{
    public List<MailboxAddress> To { get; set; }
    public List<MailboxAddress> Cc { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public Message(IEnumerable<string> to, IEnumerable<string> cc, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(email => new MailboxAddress(email, email))); // Assuming email as both name and email

        Cc = new List<MailboxAddress>();
        Cc.AddRange(cc.Select(email => new MailboxAddress(email, email))); // Assuming email as both name and email

        Subject = subject;
        Content = content;
    }
}
public class EmailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
    public string FromDisplayName { get; set; }
}
