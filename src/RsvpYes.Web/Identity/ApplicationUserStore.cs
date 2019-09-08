using Microsoft.AspNetCore.Identity;
using RsvpYes.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Web.Identity
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>
    {
        private readonly IIdentityRepository _identityRepository;

        public ApplicationUserStore(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public void Dispose()
        {
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var identity = await _identityRepository.FindByIdAsync(new IdentityId(new Guid(userId))).ConfigureAwait(false);
            return new ApplicationUser()
            {
                Id = identity.Id.Value.ToString(),
                UserName = identity.AccountName,
            };
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var identity = await _identityRepository.FindByAccountNameAsync(normalizedUserName).ConfigureAwait(false);
            if (identity == null)
            {
                return null;
            }
            return new ApplicationUser()
            {
                Id = identity.Id.Value.ToString(),
                UserName = identity.AccountName,
            };
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName.ToUpperInvariant());
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
