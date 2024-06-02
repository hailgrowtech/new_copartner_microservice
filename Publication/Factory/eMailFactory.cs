using MimeKit;
using System.Net;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Publication.Factory
{
    public class eMailFactory
    {
        private readonly string _server;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _from;
        private readonly ILogger<eMailFactory> _logger;

        public eMailFactory(ILogger<eMailFactory> logger)
        {
            var emailSettings = ConfigHelper.GetEmailSettings();
            _server = emailSettings.SmtpServer;
            _port = emailSettings.Port;
            _username = emailSettings.Username;
            _password = emailSettings.Password;
            _from = emailSettings.From;
            _logger = logger;
        }

        public async Task<HttpStatusCode> PostEmailAsync(string[] to, string[] cc, string subject, string content)
        {
            try
            {
                var message = new Message(to, cc, subject, content);
                var statusCode = await SendEmailAsync(message);
                return statusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting email");
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            try
            {
                await SendAsync(emailMessage);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                return HttpStatusCode.InternalServerError;
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_from, _from));
            emailMessage.To.AddRange(message.To);
            emailMessage.Cc.AddRange(message.Cc);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    _logger.LogInformation("Connecting to SMTP server...");
                    await client.ConnectAsync(_server, _port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    _logger.LogInformation("Authenticating...");
                    await client.AuthenticateAsync(_username, _password);

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
}
