using MediatR;
using RsvpYes.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Users
{
    public class IdentityRegisterWithoutUserCommandHandler : IRequestHandler<IdentityRegisterWithoutUserCommand>
    {
        private readonly IIdentityRepository _repository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;

        public IdentityRegisterWithoutUserCommandHandler(
            IIdentityRepository repository,
            IOrganizationRepository organizationRepository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(IdentityRegisterWithoutUserCommand command, CancellationToken cancellationToken = default)
        {
            var organization = default(Organization);
            if (!string.IsNullOrEmpty(command.OrganizationName))
            {
                organization = await _organizationRepository.FindByNameAsync(command.OrganizationName).ConfigureAwait(false);
                if (organization == null)
                {
                    organization = new Organization(command.OrganizationName);
                    await _organizationRepository.SaveAsync(organization).ConfigureAwait(false);
                }
            }

            var user = new User(command.UserName, command.UserMailAddress, organization?.Id);
            await _userRepository.SaveAsync(user).ConfigureAwait(false);

            var identity = new Identity(command.AccountName, command.PasswordHash, user.Id);
            await _repository.SaveAsync(identity).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
