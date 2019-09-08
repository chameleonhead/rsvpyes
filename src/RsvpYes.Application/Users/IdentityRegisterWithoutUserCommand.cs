using MediatR;
using RsvpYes.Domain.Users;

namespace RsvpYes.Application.Users
{
    public class IdentityRegisterWithoutUserCommand : IRequest
    {
        public IdentityRegisterWithoutUserCommand(string accountName, string passwordHash, string userName, string organizationName, MailAddress userMailAddress)
        {
            AccountName = accountName;
            PasswordHash = passwordHash;
            UserName = userName;
            OrganizationName = organizationName;
            UserMailAddress = userMailAddress;
        }

        public string AccountName { get; }
        public string PasswordHash { get; }
        public string UserName { get; }
        public string OrganizationName { get; }
        public MailAddress UserMailAddress { get; }
    }
}