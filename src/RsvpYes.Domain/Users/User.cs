using System;
using System.Collections.Generic;
using System.Linq;

namespace RsvpYes.Domain.Users
{
    public class User
    {
        private readonly List<MailAddress> _mailAddresses;
        private readonly List<PhoneNumber> _phoneNumbers;

        public User(string name, MailAddress mailAddress, OrganizationId organizationId)
        {
            _mailAddresses = new List<MailAddress>();
            _phoneNumbers = new List<PhoneNumber>();
            Id = new UserId();
            Name = name;
            OrganizationId = organizationId;
            DefaultMailAddress = mailAddress;
            if (mailAddress != null)
            {
                _mailAddresses.Add(mailAddress);
            }
        }

        public User(
            UserId userId,
            string name,
            MailAddress mailAddress,
            OrganizationId organizationId,
            IEnumerable<MailAddress> mailAddresses,
            IEnumerable<PhoneNumber> phoneNumbers,
            string messageSignature)
        {
            Id = userId;
            Name = name;
            DefaultMailAddress = mailAddress;
            OrganizationId = organizationId;
            _mailAddresses = mailAddresses.ToList();
            _phoneNumbers = phoneNumbers.ToList();
            MessageSignature = messageSignature;
        }

        public UserId Id { get; }
        public string Name { get; private set; }
        public string MessageSignature { get; private set; }

        public OrganizationId OrganizationId { get; private set; }
        public MailAddress DefaultMailAddress { get; private set; }
        public IEnumerable<MailAddress> MailAddresses => _mailAddresses.ToList();
        public IEnumerable<PhoneNumber> PhoneNumbers => _phoneNumbers.ToList();

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateOrganizationId(OrganizationId organizationId)
        {
            OrganizationId = organizationId;
        }

        public void UpdateMessageSignature(string signature)
        {
            MessageSignature = signature?.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        public void SetDefaultMailAddress(MailAddress mailAddress)
        {
            if (!_mailAddresses.Contains(mailAddress))
            {
                throw new InvalidOperationException(Constants.UserMailAddressNotExistsError);
            }
            DefaultMailAddress = mailAddress;
        }

        public void AddMailAddress(MailAddress mailAddress)
        {
            if (_mailAddresses.Count >= 5)
            {
                throw new InvalidOperationException(Constants.UserMailAddressCanStoreLessThanOrEqualToFiveError);
            }
            _mailAddresses.Add(mailAddress);
        }

        public void RemoveMailAddress(MailAddress mailAddress)
        {
            if (!_mailAddresses.Contains(mailAddress))
            {
                throw new InvalidOperationException(Constants.UserMailAddressNotExistsError);
            }
            if (_mailAddresses.Count == 1)
            {
                throw new InvalidOperationException(Constants.UserMailAddressMustHaveAtLeastOneError);
            }
            _mailAddresses.Remove(mailAddress);
            SetDefaultMailAddressIfNeeded();
        }

        private void SetDefaultMailAddressIfNeeded()
        {
            if (_mailAddresses.Count == 0)
            {
                DefaultMailAddress = null;
            }
            else if (_mailAddresses.Contains(DefaultMailAddress))
            {
                return;
            }
            else
            {
                DefaultMailAddress = _mailAddresses[0];
            }
        }

        public void AddPhoneNumber(PhoneNumber phoneNumber)
        {
            if (_phoneNumbers.Count >= 5)
            {
                throw new InvalidOperationException(Constants.UserPhoneNumberCanStoreLessThanOrEqualToFiveError);
            }
            _phoneNumbers.Add(phoneNumber);
        }

        public void RemovePhoneNumber(PhoneNumber phoneNumber)
        {
            if (!_phoneNumbers.Contains(phoneNumber))
            {
                throw new InvalidOperationException(Constants.UserPhoneNumberNotExistsError);
            }
            _phoneNumbers.Remove(phoneNumber);
        }
    }
}
