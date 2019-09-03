using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Messaging
{
    public class MessageId
    {
        public MessageId()
        {
            Value = Guid.NewGuid();
        }

        public MessageId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is MessageId id &&
                   Value.Equals(id.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<Guid>.Default.GetHashCode(Value);
        }
    }
}