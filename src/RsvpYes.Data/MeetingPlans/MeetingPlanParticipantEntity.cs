using System;

namespace RsvpYes.Data.MeetingPlans
{
    public class MeetingPlanParticipantEntity
    {
        public Guid MeetingId { get; set; }
        public Guid UserId { get; set; }
        public MeetingPlanParticipantRole Role { get; set; }
    }
}
