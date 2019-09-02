using RsvpYes.Domain;

namespace RsvpYes.Application
{
    public class MeetingPlanUpdateMeetingNameCommand
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