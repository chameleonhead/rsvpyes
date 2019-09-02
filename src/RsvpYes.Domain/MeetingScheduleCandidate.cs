using System;

namespace RsvpYes.Domain
{
    public class MeetingScheduleCandidate
    {
        public MeetingScheduleCandidate(MeetingSchedule schedule)
        {
            Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
        }

        public MeetingSchedule Schedule { get; }
    }
}
