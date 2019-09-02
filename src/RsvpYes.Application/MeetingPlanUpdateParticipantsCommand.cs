using RsvpYes.Domain;
using System.Collections.Generic;

namespace RsvpYes.Application
{
    public class MeetingPlanUpdateParticipantsCommand
    {
        public MeetingPlanUpdateParticipantsCommand(MeetingId meetingId)
        {
            MeetingId = meetingId;
        }

        public MeetingId MeetingId { get; set; }
        public List<UserId> ParticipantsToRemove { get; } = new List<UserId>();
        public List<UserId> MainGuests { get; } = new List<UserId>();
        public List<UserId> Guests { get; } = new List<UserId>();
    }
}