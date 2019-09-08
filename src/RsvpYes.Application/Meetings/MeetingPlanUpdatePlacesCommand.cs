using MediatR;
using RsvpYes.Domain.Meetings;
using System.Collections.Generic;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdatePlacesCommand : IRequest
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