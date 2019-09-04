using RsvpYes.Domain.Users;
using System;

namespace RsvpYes.Domain.Messaging
{
    public class RequestMessage : IMessage
    {
        public RequestMessage(User from, User to, string message, string responseUrl)
        {
            Id = new MessageId();
            From = from;
            To = to;
            Message = message;
            ResponseUrl = responseUrl;
        }

        public MessageId Id { get; }
        public User From { get; }
        public User To { get; }
        public string Message { get; }
        public string ResponseUrl { get; }

        public string RenderMessageBody()
        {
            var signature = From.MessageSignature != null
                ? string.Concat(Environment.NewLine, From.MessageSignature, Environment.NewLine)
                : "";
            return string.Concat($"{To.Name} 様", Environment.NewLine,
                Environment.NewLine,
                Message, Environment.NewLine,
                Environment.NewLine,
                $"回答は以下のURLよりお願いいたします。", Environment.NewLine,
                ResponseUrl, Environment.NewLine,
                Environment.NewLine,
                "以上", Environment.NewLine,
                signature);
        }
    }
}