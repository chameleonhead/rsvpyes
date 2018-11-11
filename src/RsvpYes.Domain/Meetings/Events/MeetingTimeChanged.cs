using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Meetings.Events
{
    public class MeetingTimeChanged : DomainEvent<Meeting>
    {
        public MeetingTime Time { get; }
        public MeetingTime OldTime { get; }

        public MeetingTimeChanged(Meeting entity, MeetingTime time, MeetingTime oldTime) : base(entity)
        {
            Time = time;
            OldTime = oldTime;
        }
    }
}