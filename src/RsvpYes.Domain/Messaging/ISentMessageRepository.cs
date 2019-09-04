using System.Threading.Tasks;

namespace RsvpYes.Domain.Messaging
{
    public interface ISentMessageRepository
    {
        Task<SentMessage> FindByIdAsync(MessageId messageId);
        Task SaveAsync(SentMessage message);
    }
}
