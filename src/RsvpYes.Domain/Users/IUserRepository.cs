using System.Threading.Tasks;

namespace RsvpYes.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(UserId userId);
        Task SaveAsync(User user);
    }
}
