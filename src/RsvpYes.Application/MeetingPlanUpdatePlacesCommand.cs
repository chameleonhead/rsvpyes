using RsvpYes.Domain.Meetings;
using System.Collections.Generic;

namespace RsvpYes.Application
{
    public class MeetingPlanUpdatePlacesCommand
    {
        public MeetingPlanUpdatePlacesCommand(MeetingId meetingId)
        {
            MeetingId = meetingId;
        }
        public MeetingId MeetingId { get; }
        public List<MeetingPlace> PlacesToRemove { get; } = new List<MeetingPlace>();
        public List<MeetingPlace> PlacesToAdd { get; } = new List<MeetingPlace>();
    }
}