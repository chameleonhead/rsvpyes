using System.Collections.Generic;

namespace RsvpYes.Domain
{
    public class Url
    {
        public Url(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return obj is Url url &&
                   Value == url.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }
}