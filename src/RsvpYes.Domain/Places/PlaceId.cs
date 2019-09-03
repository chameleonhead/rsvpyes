using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Places
{
    public class PlaceId
    {
        public PlaceId()
        {
            Value = Guid.NewGuid();
        }

        public PlaceId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is PlaceId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}
