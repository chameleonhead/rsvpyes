using RsvpYes.Domain;

namespace RsvpYes.Application
{
    public class MeetingPlanUpdateCommand
    {
        public MeetingPlanUpdateCommand(MeetingId meetingId, string meetingName)
        {
            MeetingId = meetingId;
            MeetingName = meetingName;
        }

        public MeetingId MeetingId { get; }
        public string MeetingName { get; }
    }
}