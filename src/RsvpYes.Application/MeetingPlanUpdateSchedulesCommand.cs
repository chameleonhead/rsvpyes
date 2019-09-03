using RsvpYes.Domain.Meetings;
using System.Collections.Generic;

namespace RsvpYes.Application
{
    public class MeetingPlanUpdateSchedulesCommand
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
