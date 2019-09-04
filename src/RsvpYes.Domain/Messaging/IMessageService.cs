using System.Threading.Tasks;

namespace RsvpYes.Domain.Messaging
{
    public interface IMessageService
    {
        Task SendAsync(IMessage message);
    }
}