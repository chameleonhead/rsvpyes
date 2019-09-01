namespace RsvpYes.Domain
{
    public class MeetingPlaceCandidate
    {
        public MeetingPlaceCandidate(MeetingId meetingId, MeetingPlace place)
        {
            MeetingId = meetingId;
            Place = place;
        }

        public MeetingId MeetingId { get; }
        public MeetingPlace Place { get; }
    }
}