using MediatR;
using RsvpYes.Domain.Meetings;
using System.Collections.Generic;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdateSchedulesCommand : IRequest
    {
        public MeetingPlanUpdateSchedulesCommand(MeetingId meetingId)
        {
            MeetingId = meetingId;
        }

        public MeetingId MeetingId { get; }
        public List<MeetingSchedule> SchedulesToAdd { get; } = new List<MeetingSchedule>();
        public List<MeetingSchedule> SchedulesToRemove { get; } = new List<MeetingSchedule>();
    }
}
