using System;

namespace RsvpYes.Domain
{
    public class MeetingScheduleCandidate
    {
        public MeetingScheduleCandidate(MeetingId meetingId, MeetingSchedule schedule)
        {
            MeetingId = meetingId ?? throw new ArgumentNullException(nameof(meetingId));
            Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
        }

        public MeetingId MeetingId { get; }
        public MeetingSchedule Schedule { get; }
    }
}
