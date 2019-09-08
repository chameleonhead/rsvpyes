using MediatR;
using RsvpYes.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Users
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserId>
    {
        private readonly IUserRepository _repository;

        public UserCreateCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserId> Handle(UserCreateCommand command, CancellationToken cancellation = default)
        {
            var user = new User(command.UserName, command.UserMailAddress, command.UserOrganizationId);
            await _repository.SaveAsync(user);
            return user.Id;
        }
    }
}
