using System;
using System.Collections.Generic;
using System.Linq;

namespace RsvpYes.Domain
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

        public UserId Id { get; }
        public string Name { get; private set; }
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
