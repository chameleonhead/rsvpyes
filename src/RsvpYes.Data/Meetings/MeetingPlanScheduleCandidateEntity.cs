using System;

namespace RsvpYes.Data.Meetings
{
    internal class MeetingPlanScheduleCandidateEntity
    {
        public Guid MeetingId { get; set; }
        public int Seq { get; set; }
        public DateTime BeginAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
