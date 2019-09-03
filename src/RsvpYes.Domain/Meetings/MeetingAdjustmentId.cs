using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class MeetingAdjustmentId
    {
        public MeetingAdjustmentId()
        {
            Value = Guid.NewGuid();
        }

        public MeetingAdjustmentId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is MeetingAdjustmentId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}
