namespace RsvpYes.Domain.Meetings
{
    public class MeetingPlaceCandidate
    {
        public MeetingPlaceCandidate(MeetingPlace place)
        {
            Place = place;
        }

        public MeetingPlace Place { get; }
    }
}