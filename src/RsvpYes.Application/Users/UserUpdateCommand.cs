using MediatR;
using RsvpYes.Domain.Users;
using System.Collections.Generic;

namespace RsvpYes.Application.Users
{
    public class UserUpdateCommand : IRequest
    {
        public UserUpdateCommand(UserId userId, string userName, MailAddress userDefaultMailAddress, OrganizationId organizationId, string userMessageSignature)
        {
            UserId = userId;
            UserName = userName;
            UserDefaultMailAddress = userDefaultMailAddress;
            UserOrganizationId = organizationId;
            UserMessageSignature = userMessageSignature;
        }

        public UserId UserId { get; }
        public string UserName { get; }
        public MailAddress UserDefaultMailAddress { get; }
        public OrganizationId UserOrganizationId { get; }
        public string UserMessageSignature { get; }
        public List<MailAddress> UserMailAddressesToAdd { get; } = new List<MailAddress>();
        public List<MailAddress> UserMailAddressesToRemove { get; } = new List<MailAddress>();
        public List<PhoneNumber> UserPhoneNumberToAdd { get; } = new List<PhoneNumber>();
        public List<PhoneNumber> UserPhoneNumberToRemove { get; } = new List<PhoneNumber>();
    }
}
