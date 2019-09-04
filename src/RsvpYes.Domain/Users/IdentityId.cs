using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Users
{
    public class IdentityId
    {
        public IdentityId()
        {
            Value = Guid.NewGuid();
        }

        public IdentityId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is IdentityId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}
