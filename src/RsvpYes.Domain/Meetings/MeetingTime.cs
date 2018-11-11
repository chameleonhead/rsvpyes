using RsvpYes.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class MeetingTime : ValueObject
    {
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset? EndTime { get; }

        public MeetingTime(DateTimeOffset startTime)
        {
            StartTime = startTime;
        }

        public MeetingTime(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartTime;
            yield return EndTime;
        }
    }
}