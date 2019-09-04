using RsvpYes.Data.Messaging;
using RsvpYes.Domain.Messaging;
using RsvpYes.Domain.Users;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class SentMessageRepository : ISentMessageRepository
    {
        private readonly RsvpYesDbContext _context;

        public SentMessageRepository(RsvpYesDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(SentMessage message)
        {
            var messageId = message.Id.Value;
            var sentMessageEntity = await _context.SentMessages.FindAsync(messageId).ConfigureAwait(false);

            if (sentMessageEntity != null)
            {
                _context.SentMessages.Remove(sentMessageEntity);
            }

            _context.SentMessages.Add(new SentMessageEntity()
            {
                Id = messageId,
                From = message.From.Value,
                FromAddress = message.FromAddress.Value,
                To = message.To.Value,
                ToAddress = message.ToAddress.Value,
                Body = message.Body,
                Timestamp = message.Timestamp
            }) ;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<SentMessage> FindByIdAsync(MessageId messageId)
        {
            var sentMessageEntity = await _context.SentMessages.FindAsync(messageId.Value).ConfigureAwait(false);

            if (sentMessageEntity == null)
            {
                return null;
            }

            return new SentMessage(
                messageId,
                new UserId(sentMessageEntity.From),
                new MailAddress(sentMessageEntity.FromAddress),
                new UserId(sentMessageEntity.To),
                new MailAddress(sentMessageEntity.ToAddress),
                sentMessageEntity.Body,
                sentMessageEntity.Timestamp);
        }
    }
}
