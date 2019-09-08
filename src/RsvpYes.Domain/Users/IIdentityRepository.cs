using System.Threading.Tasks;

namespace RsvpYes.Domain.Users
{
    public interface IIdentityRepository
    {
        Task<Identity> FindByIdAsync(IdentityId identityId);
        Task<Identity> FindByAccountNameAndPasswordAsync(string accountName, string passwordHash);
        Task<Identity> FindByAccountNameAsync(string accountName);
        Task SaveAsync(Identity identity);
    }
}
