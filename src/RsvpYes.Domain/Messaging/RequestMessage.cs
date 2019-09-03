using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Messaging
{
    public class RequestMessage
    {
        public RequestMessage(User from, User to, string message)
        {
            Id = new MessageId();
            From = from;
            To = to;
            Message = message;
        }

        public MessageId Id { get; }
        public User From { get; }
        public User To { get; }
        public string Message { get; }
    }
}