using rsvpyes.Data;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace rsvpyes.Services
{
    public interface IMailService
    {
        Task Send(MailSendCommand command);
    }

    public interface IMailConfiguration
    {
        string Host { get; }
        int Port { get; }
        string Username { get; }
        string Password { get; }
        string Signature { get; }
    }

    public class MailConfiguration : IMailConfiguration
    {
        public MailConfiguration(
            string host,
            int port,
            string username,
            string password,
            string signature)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
            Signature = signature;
        }

        public string Host { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
        public string Signature { get; }
    }

    public sealed class MailSendCommand
    {
        public Guid MeetingId { get; set; }
        public Guid SenderId { get; set; }
        public Guid[] RecipiantUserIds { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ResponseUri { get; set; }
    }

    public class MailService : IMailService
    {
        private IDataService<User> usersService;
        private IDataService<RsvpRequest> rsvpRequestsService;
        private IMailConfiguration configuration;

        public MailService(
            IDataService<User> usersService,
            IDataService<RsvpRequest> rsvpRequestsService,
            IMailConfiguration configuration)
        {
            this.usersService = usersService;
            this.rsvpRequestsService = rsvpRequestsService;
            this.configuration = configuration;
        }
        public async Task Send(MailSendCommand command)
        {
            var sender = await usersService.Find(command.SenderId);

            using (var client = new SmtpClient(configuration.Host, configuration.Port))
            {
                client.Credentials = new NetworkCredential(configuration.Username, configuration.Password);
                foreach (var userId in command.RecipiantUserIds)
                {
                    var rsvpRequest = await rsvpRequestsService.Insert(new RsvpRequest()
                    {
                        MeetingId = command.MeetingId,
                        UserId = userId,
                    });

                    var to = await usersService.Find(userId);
                    var mailMessage = new MailMessage(sender.Email, to.Email);
                    mailMessage.Subject = command.Title;
                    mailMessage.Body = CreateMessage(to.Name, command.Message, command.ResponseUri, rsvpRequest.Id);
                    client.Send(mailMessage);
                }
            }
        }

        private string CreateMessage(string recipientName, string message, string responseUri, Guid responseId)
        {
            return $@"{recipientName}様

{message}

返信は以下のURLよりお願いいたします。
{responseUri}?id={responseId}

以上

{configuration.Signature}";
        }
    }
}
