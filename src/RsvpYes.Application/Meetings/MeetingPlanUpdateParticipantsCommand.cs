using MediatR;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Users;
using System.Collections.Generic;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdateParticipantsCommand : IRequest
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