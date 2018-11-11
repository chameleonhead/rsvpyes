using RsvpYes.Domain.SeedWork;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class PartitipationFee : ValueObject
    {
        public decimal Value { get; }

        public PartitipationFee(decimal value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}