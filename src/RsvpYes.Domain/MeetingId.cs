using System;
using System.Collections.Generic;

namespace RsvpYes.Domain
{
    public class MeetingId
    {
        public MeetingId()
        {
            Value = Guid.NewGuid();
        }

        internal MeetingId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is MeetingId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}