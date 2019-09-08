using MediatR;
using RsvpYes.Domain.Meetings;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdateMeetingNameCommand : IRequest
    {
        public MeetingPlanUpdateMeetingNameCommand(MeetingId meetingId, string meetingName)
        {
            MeetingId = meetingId;
            MeetingName = meetingName;
        }

        public MeetingId MeetingId { get; }
        public string MeetingName { get; }
    }
}