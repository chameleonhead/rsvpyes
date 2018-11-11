using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Meetings.Events
{
    public class MeetingPlaceChanged : DomainEvent<Meeting>
    {
        public MeetingPlace Place { get; }
        public MeetingPlace OldPlace { get; }

        public MeetingPlaceChanged(Meeting entity, MeetingPlace place, MeetingPlace oldPlace) : base(entity)
        {
            Place = place;
            OldPlace = oldPlace;
        }
    }
}