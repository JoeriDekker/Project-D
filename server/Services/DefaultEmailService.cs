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
            // Send email
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Water Allocation Manager", ""));
            mailMessage.To.Add(new MailboxAddress(to, to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var smtpClient = new SmtpClient())
            {
                
                smtpClient.Connect(_configuration.GetValue<string>("Email:Host"), _configuration.GetValue<int>("Email:Port"), true);
                smtpClient.Authenticate(_configuration.GetValue<string>("Email:Username"), _configuration.GetValue<string>("Email:Password"));
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}