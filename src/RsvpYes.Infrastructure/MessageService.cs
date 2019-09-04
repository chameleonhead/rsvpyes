using RsvpYes.Domain.Messaging;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RsvpYes.Infrastructure
{
    public class MessageService : IMessageService
    {
        private readonly MessageServiceOptions _options;

        public MessageService(MessageServiceOptions options)
        {
            _options = options;
        }

        public async Task SendAsync(IMessage message)
        {
            using (var client = new SmtpClient(_options.Host, _options.Port))
            {
                client.Credentials = new NetworkCredential(_options.Username, _options.Password);
                var mailMessage = new MailMessage(
                    message.From.DefaultMailAddress.Value,
                    message.To.DefaultMailAddress.Value)
                {
                    Subject = message.Title,
                    Body = message.Body
                };
                await client.SendMailAsync(mailMessage).ConfigureAwait(false);
            }
        }
    }
}
