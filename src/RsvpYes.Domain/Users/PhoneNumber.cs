using System.Collections.Generic;

namespace RsvpYes.Domain.Users
{
    public class PhoneNumber
    {
        public PhoneNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return obj is PhoneNumber number &&
                   Value == number.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }
}