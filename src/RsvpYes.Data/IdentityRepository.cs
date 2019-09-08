using Microsoft.EntityFrameworkCore;
using RsvpYes.Data.Users;
using RsvpYes.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly RsvpYesDbContext _context;

        public IdentityRepository(RsvpYesDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Identity identity)
        {
            var identityId = identity.Id.Value;
            if (await _context.Identities.AnyAsync(e => e.Id != identityId && e.AccountName == identity.AccountName))
            {
                throw new InvalidOperationException(Constants.IdentityAccountNameMustUniqueError);
            }
            var identityEntity = await _context.Identities.FindAsync(identityId).ConfigureAwait(false);

            if (identityEntity != null)
            {
                _context.Identities.Remove(identityEntity);
            }

            _context.Identities.Add(new IdentityEntity()
            {
                Id = identityId,
                AccountName = identity.AccountName,
                PasswordHash = identity.PasswordHash,
                UserId = identity.UserId.Value
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Identity> FindByAccountNameAndPasswordAsync(string accountName, string passwordHash)
        {
            var identityEntity = await _context.Identities
                .Where(e => e.AccountName.ToUpperInvariant() == accountName.ToUpperInvariant() && e.PasswordHash == passwordHash)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return ConvertToIdentity(identityEntity);
        }

        public async Task<Identity> FindByAccountNameAsync(string accountName)
        {
            var identityEntity = await _context.Identities
                .Where(e => e.AccountName.ToUpperInvariant() == accountName.ToUpperInvariant())
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return ConvertToIdentity(identityEntity);
        }

        public async Task<Identity> FindByIdAsync(IdentityId identityId)
        {
            var identityEntity = await _context.Identities.FindAsync(identityId.Value).ConfigureAwait(false);
            return ConvertToIdentity(identityEntity);
        }

        private static Identity ConvertToIdentity(IdentityEntity identityEntity)
        {
            if (identityEntity == null)
            {
                return null;
            }

            return new Identity(
                new IdentityId(identityEntity.Id),
                identityEntity.AccountName,
                identityEntity.PasswordHash,
                identityEntity.UserId.HasValue ? new UserId(identityEntity.UserId.Value) : null);
        }
    }
}
