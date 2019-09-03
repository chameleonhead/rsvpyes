using System.Threading.Tasks;

namespace RsvpYes.Domain.Messaging
{
    public interface IMessageRepository
    {
        Task<IMessage> FindByIdAsync(MessageId messageId);
        Task SaveAsync(IMessage message);
    }
}
