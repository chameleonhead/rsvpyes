using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Messaging
{
    public abstract class MessageBase : IMessage
    {
        private string _title;
        private string _body;

        protected MessageBase(User from, User to)
        {
            Id = new MessageId();
            From = from;
            To = to;
        }

        public MessageId Id { get; }
        public User From { get; }
        public User To { get; }
        public string Title => _title ?? (_title = RenderTitle());
        public string Body => _body ?? (_body = RenderBody());
        protected abstract string RenderTitle();
        protected abstract string RenderBody();
    }
}
