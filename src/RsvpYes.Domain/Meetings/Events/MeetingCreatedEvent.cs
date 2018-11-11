using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Meetings.Events
{
    public class MeetingCreatedEvent : DomainEvent<Meeting>
    {
        public string Name { get; }
        public MeetingTime Time { get; }
        public MeetingPlace Place { get; }
        public MeetingDetails Details { get; }

        public MeetingCreatedEvent(Meeting entity, string name, MeetingTime time, MeetingPlace place, MeetingDetails details) : base(entity)
        {
            Name = name;
        }
    }
}