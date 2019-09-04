using RsvpYes.Domain.Users;
using System;

namespace RsvpYes.Domain.Messaging
{
    public class SentMessage
    {
        public SentMessage(
            MessageId messageId, 
            UserId from, 
            MailAddress fromAddress, 
            UserId to, 
            MailAddress toAddress, 
            string body)
        {
            Id = messageId;
            From = from;
            FromAddress = fromAddress;
            To = to;
            ToAddress = toAddress;
            Body = body;
            Timestamp = DateTime.UtcNow;
        }

        public SentMessage(
            MessageId messageId,
            UserId from,
            MailAddress fromAddress,
            UserId to,
            MailAddress toAddress,
            string body,
            DateTime timestamp)
        {
            Id = messageId;
            From = from;
            FromAddress = fromAddress;
            To = to;
            ToAddress = toAddress;
            Body = body;
            Timestamp = timestamp;
        }

        public MessageId Id { get; }
        public UserId From { get; }
        public MailAddress FromAddress { get; }
        public UserId To { get; }
        public MailAddress ToAddress { get; }
        public string Body { get; }
        public DateTime Timestamp { get; }
    }
}
