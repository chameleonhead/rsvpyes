using RsvpYes.Domain.SeedWork;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class MeetingDetails : ValueObject
    {
        public PartitipationFee Fee { get; }

        public MeetingDetails(PartitipationFee partitipationFee)
        {
            Fee = partitipationFee;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Fee;
        }
    }
}