using System;
using System.Collections.Generic;

namespace RsvpYes.Domain
{
    public class OrganizationId
    {
        public OrganizationId()
        {
            Value = Guid.NewGuid();
        }

        public OrganizationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is OrganizationId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}
