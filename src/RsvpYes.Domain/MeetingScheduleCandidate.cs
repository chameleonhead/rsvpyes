using System.Diagnostics.Contracts;

namespace RsvpYes.Domain
{
    public class MeetingScheduleCandidate
    {
        public MeetingScheduleCandidate(MeetingId meetingId, MeetingSchedule schedule)
        {
            Contract.Requires(meetingId != null);
            Contract.Requires(schedule != null);
            MeetingId = meetingId;
            Schedule = schedule;
        }

        public MeetingId MeetingId { get; }
        public MeetingSchedule Schedule { get; }
    }
}
