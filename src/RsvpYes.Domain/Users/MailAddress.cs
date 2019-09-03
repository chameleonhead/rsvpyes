using System.Collections.Generic;

namespace RsvpYes.Domain.Users
{
    public class MailAddress
    {
        public MailAddress(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return obj is MailAddress address &&
                   Value == address.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }
}