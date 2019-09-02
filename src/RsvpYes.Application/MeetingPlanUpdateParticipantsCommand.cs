using RsvpYes.Domain;
using System.Collections.Generic;

namespace RsvpYes.Application
{
    public class MeetingPlanUpdateParticipantsCommand
    {
        public MeetingId MeetingId { get; set; }
        public IEnumerable<UserId> ParticipantsToRemove { get; } = new List<UserId>();
        public IEnumerable<UserId> MainGuests { get; } = new List<UserId>();
        public IEnumerable<UserId> Guests { get; } = new List<UserId>();
    }
}