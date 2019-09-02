using System;

namespace RsvpYes.Data.MeetingPlans
{
    public class MeetingPlanScheduleCandidateEntity
    {
        public Guid MeetingId { get; set; }
        public int Seq { get; set; }
        public DateTime BeginAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
