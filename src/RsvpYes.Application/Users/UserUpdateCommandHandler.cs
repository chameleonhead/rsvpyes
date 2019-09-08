using MediatR;
using RsvpYes.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Users
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand>
    {
        private readonly IUserRepository _repository;

        public UserUpdateCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UserUpdateCommand command, CancellationToken cancellation = default)
        {
            var user = await _repository.FindByIdAsync(command.UserId).ConfigureAwait(false);
            user.UpdateName(command.UserName);
            user.UpdateOrganizationId(command.UserOrganizationId);
            user.UpdateMessageSignature(command.UserMessageSignature);
            foreach (var mail in command.UserMailAddressesToRemove)
            {
                user.RemoveMailAddress(mail);
            }
            foreach(var mail in command.UserMailAddressesToAdd)
            {
                user.AddMailAddress(mail);
            }
            user.SetDefaultMailAddress(command.UserDefaultMailAddress);

            foreach (var phone in command.UserPhoneNumberToRemove)
            {
                user.RemovePhoneNumber(phone);
            }
            foreach (var phone in command.UserPhoneNumberToAdd)
            {
                user.AddPhoneNumber(phone);
            }
            await _repository.SaveAsync(user).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
