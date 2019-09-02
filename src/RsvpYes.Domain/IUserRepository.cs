using System.Threading.Tasks;

namespace RsvpYes.Domain
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(UserId userId);
        Task SaveAsync(User user);
    }
}
