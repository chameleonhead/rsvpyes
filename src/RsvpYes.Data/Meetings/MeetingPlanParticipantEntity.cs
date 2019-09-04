using System;

namespace RsvpYes.Data.Meetings
{
    internal class MeetingPlanParticipantEntity
    {
        public Guid MeetingId { get; set; }
        public Guid UserId { get; set; }
        public MeetingPlanParticipantRole Role { get; set; }
    }
}
