using MailKit.Net.Smtp;
using MimeKit;
using WAMServer.Interfaces;

namespace WAMServer.Services
{
    public class DefaultEmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public DefaultEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            // Check if settings are configured
            string? host = _configuration.GetValue<string>("Email:Host");
            int port = _configuration.GetValue<int>("Email:Port");
            if (string.IsNullOrEmpty(host))
            {
                throw new Exception("Email host is not set in configuration");
            }

            if (port == 0)
            {
                throw new Exception("Email port is not set in configuration");
            }

            string? username = _configuration.GetValue<string>("Email:Username");
            string? password = _configuration.GetValue<string>("Email:Password");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Email username and/or password is not set in configuration");
            }
            // Send email
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("WaterAllocationManager", username));
            mailMessage.To.Add(new MailboxAddress(to, to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = body
            };


            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_configuration.GetValue<string>("Email:Host"), _configuration.GetValue<int>("Email:Port"), false);
                smtpClient.Authenticate(username, password);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}