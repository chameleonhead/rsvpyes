using MediatR;
using RsvpYes.Domain.Users;

namespace RsvpYes.Application.Users
{
    public class UserCreateCommand : IRequest<UserId>
    {
        public UserCreateCommand(string userName, MailAddress userMailAddress, OrganizationId userOrganizationId)
        {
            UserName = userName;
            UserMailAddress = userMailAddress;
            UserOrganizationId = userOrganizationId;
        }

        public string UserName { get; }
        public MailAddress UserMailAddress { get; }
        public OrganizationId UserOrganizationId { get; }
    }
}