using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Messaging
{
    public interface IMessage
    {
        MessageId Id { get; }
        User From { get; }
        User To { get; }
        string Title { get; }
        string Body { get; }
    }
}
