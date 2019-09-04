using RsvpYes.Domain.Users;
using System.Threading.Tasks;

namespace RsvpYes.Application
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserId> CreateAsync(UserCreateCommand command)
        {
            var user = new User(command.UserName, command.UserMailAddress, command.UserOrganizationId);
            await _repository.SaveAsync(user);
            return user.Id;
        }

        public async Task UpdateAsync(UserUpdateCommand command)
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
        }
    }
}
