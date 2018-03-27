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
        string ResponseUri { get; }
    }

    public class MailConfiguration : IMailConfiguration
    {
        public MailConfiguration(
            string host,
            int port,
            string username,
            string password,
            string signature,
            string responseUri)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
            Signature = signature;
            ResponseUri = responseUri;
        }

        public string Host { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
        public string Signature { get; }
        public string ResponseUri { get; }
    }

    public sealed class MailSendCommand
    {
        public Guid MeetingId { get; set; }
        public Guid SenderId { get; set; }
        public Guid[] RecipiantUserIds { get; set; }
        public string Message { get; set; }
    }

    public class MailService : IMailService
    {
        private IDataService<User> usersService;
        private IDataService<Meeting> meetingsService;
        private IDataService<RsvpRequest> rsvpRequestsService;
        private IMailConfiguration configuration;

        public MailService(
            IDataService<User> usersService,
            IDataService<Meeting> meetingsService,
            IDataService<RsvpRequest> rsvpRequestsService,
            IMailConfiguration configuration)
        {
            this.usersService = usersService;
            this.meetingsService = meetingsService;
            this.rsvpRequestsService = rsvpRequestsService;
            this.configuration = configuration;
        }
        public async Task Send(MailSendCommand command)
        {
            var meeting = await meetingsService.Find(command.MeetingId);
            var sender = await usersService.Find(command.SenderId);
            string message = CreateInvitationMessage(command, meeting);

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
                    mailMessage.Subject = $"{meeting.Name}への誘い";
                    mailMessage.Body = CreateMessage(to.Name, message, rsvpRequest.Id);
                    client.Send(mailMessage);
                }
            }
        }

        private static string CreateInvitationMessage(MailSendCommand command, Meeting meeting)
        {
            return $@"{command.Message}

        記

日時
{meeting.StartTime.ToString("M/d (ddd) HH:mm")} ～
 
場所
{meeting.PlaceName}{(string.IsNullOrEmpty(meeting.PlaceUri) ? "" : Environment.NewLine + meeting.PlaceUri)}
 
会費
{meeting.Fee.ToString("#,#0")} 円

";
        }

        private string CreateMessage(string recipientName, string message, Guid responseId)
        {
            return $@"{recipientName}様

{message}

返信は以下のURLよりお願いいたします。
{configuration.ResponseUri}?id={responseId}

以上

{configuration.Signature}";
        }
    }
}
