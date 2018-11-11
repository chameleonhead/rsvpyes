using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Meetings.Events
{
    public class MeetingDetailsChanged : DomainEvent<Meeting>
    {
        public MeetingDetails Details { get; }
        public MeetingDetails OldDetails { get; }

        public MeetingDetailsChanged(Meeting entity, MeetingDetails details, MeetingDetails oldDetails) : base(entity)
        {
            Details = details;
            OldDetails = oldDetails;
        }
    }
}