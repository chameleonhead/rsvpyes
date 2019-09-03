using System;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class MeetingSchedule
    {
        public MeetingSchedule(DateTime startTime, TimeSpan duration)
        {
            if (duration < TimeSpan.Zero)
            {
                throw new ArgumentException(Constants.DurationMustBeGreaterOrEqualToZeroError);
            }
            BeginAt = startTime;
            EndAt = startTime + duration;
            Duration = duration;
        }

        public MeetingSchedule(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException(Constants.EndTimeMustBeGreaterOrEqualToStartTimeError);
            }
            BeginAt = startTime;
            EndAt = endTime;
            Duration = endTime - startTime;
        }

        public DateTime BeginAt { get; }
        public DateTime EndAt { get; }
        public TimeSpan Duration { get; }

        public override string ToString()
        {
            return $"Schedule: {BeginAt} - {EndAt}";
        }

        public override bool Equals(object obj)
        {
            return obj is MeetingSchedule schedule &&
                   BeginAt == schedule.BeginAt &&
                   Duration.Equals(schedule.Duration);
        }

        public override int GetHashCode()
        {
            var hashCode = 1459874656;
            hashCode = hashCode * -1521134295 + BeginAt.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TimeSpan>.Default.GetHashCode(Duration);
            return hashCode;
        }
    }
}