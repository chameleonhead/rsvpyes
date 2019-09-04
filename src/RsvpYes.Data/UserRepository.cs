using Microsoft.EntityFrameworkCore;
using RsvpYes.Data.Users;
using RsvpYes.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly RsvpYesDbContext _context;

        public UserRepository(RsvpYesDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(User user)
        {
            var userId = user.Id.Value;
            var userEntity = await _context.Users.FindAsync(userId).ConfigureAwait(false);
            var userMetadataEntities = await _context.UserMetadata.Where(u => u.UserId == userId).ToListAsync().ConfigureAwait(false);

            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
                _context.UserMetadata.RemoveRange(userMetadataEntities);
            }

            _context.Users.Add(new UserEntity()
            {
                Id = userId,
                OrganizationId = user.OrganizationId.Value,
                Name = user.Name,
                DefaultMailAddress = user.DefaultMailAddress.Value
            });

            SetMeta(userId, Constants.UserMetadataMessageSignature, user.MessageSignature);

            var i = 0;
            foreach (var item in user.MailAddresses)
            {
                switch (i++)
                {
                    case 0:
                        SetMeta(userId, Constants.UserMetadataMailAddress1, item.Value);
                        break;
                    case 1:
                        SetMeta(userId, Constants.UserMetadataMailAddress2, item.Value);
                        break;
                    case 2:
                        SetMeta(userId, Constants.UserMetadataMailAddress3, item.Value);
                        break;
                    case 3:
                        SetMeta(userId, Constants.UserMetadataMailAddress4, item.Value);
                        break;
                    case 4:
                        SetMeta(userId, Constants.UserMetadataMailAddress5, item.Value);
                        break;
                }
            }

            var j = 0;
            foreach (var item in user.PhoneNumbers)
            {
                switch (j++)
                {
                    case 0:
                        SetMeta(userId, Constants.UserMetadataPhoneNumber1, item.Value);
                        break;
                    case 1:
                        SetMeta(userId, Constants.UserMetadataPhoneNumber2, item.Value);
                        break;
                    case 2:
                        SetMeta(userId, Constants.UserMetadataPhoneNumber3, item.Value);
                        break;
                    case 3:
                        SetMeta(userId, Constants.UserMetadataPhoneNumber4, item.Value);
                        break;
                    case 4:
                        SetMeta(userId, Constants.UserMetadataPhoneNumber5, item.Value);
                        break;
                }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private void SetMeta(Guid userId, string key, string value)
        {
            _context.UserMetadata.Add(new UserMetadataEntity() { UserId = userId, Key = key, Value = value });
        }

        public async Task<User> FindByIdAsync(UserId userId)
        {
            var userEntity = await _context.Users.FindAsync(userId.Value).ConfigureAwait(false);
            if (userEntity == null)
            {
                return null;
            }
            var userMetadataEntities = await _context.UserMetadata.Where(u => u.UserId == userId.Value).ToDictionaryAsync(u => u.Key, u => u.Value).ConfigureAwait(false);

            var mailAddresses =
                (new[] {
                    Constants.UserMetadataMailAddress1,
                    Constants.UserMetadataMailAddress2,
                    Constants.UserMetadataMailAddress3,
                    Constants.UserMetadataMailAddress4,
                    Constants.UserMetadataMailAddress5
                })
                .Select(m => new
                {
                    g = userMetadataEntities.TryGetValue(m, out var value),
                    v = value
                })
                .Where(m => m.g)
                .Select(m => new MailAddress(m.v))
                .ToList();
            var phoneNumbers =
                (new[] {
                    Constants.UserMetadataPhoneNumber1,
                    Constants.UserMetadataPhoneNumber2,
                    Constants.UserMetadataPhoneNumber3,
                    Constants.UserMetadataPhoneNumber4,
                    Constants.UserMetadataPhoneNumber5
                })
                .Select(m => new
                {
                    g = userMetadataEntities.TryGetValue(m, out var value),
                    v = value
                })
                .Where(m => m.g)
                .Select(m => new PhoneNumber(m.v))
                .ToList();
            userMetadataEntities.TryGetValue(Constants.UserMetadataMessageSignature, out var messageSignature);

            var user = new User(
                userId,
                userEntity.Name,
                new MailAddress(userEntity.DefaultMailAddress),
                new OrganizationId(userEntity.OrganizationId),
                mailAddresses,
                phoneNumbers,
                messageSignature);
            return user;
        }
    }
}
