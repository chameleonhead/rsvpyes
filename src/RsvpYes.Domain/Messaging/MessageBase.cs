using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Messaging
{
    public abstract class MessageBase : IMessage
    {
        private string _messageBody;

        protected MessageBase(User from, User to)
        {
            Id = new MessageId();
            From = from;
            To = to;
        }

        public MessageId Id { get; }
        public User From { get; }
        public User To { get; }
        public string MessageBody => _messageBody ?? (_messageBody = RenderMessageBody());
        protected abstract string RenderMessageBody();
    }
}
