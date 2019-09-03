using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Users
{
    public class UserId
    {
        public UserId()
        {
            Value = Guid.NewGuid();
        }

        public UserId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is UserId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}
