using System.Threading.Tasks;

namespace RsvpYes.Domain.Messaging
{
    interface IMessageService
    {
        Task SendAsync(IMessage message);
    }
}