using System.Collections.Generic;

namespace RsvpYes.Domain
{
    public class Meeting
    {
        public Meeting(MeetingId id, string name, MeetingSchedule schedule, MeetingPlace place, IEnumerable<Participant> participants)
        {
            Id = id;
            Name = name;
            Schedule = schedule;
            Place = place;
            Participants = new List<Participant>();
            Participants.AddRange(participants);
        }

        public MeetingId Id { get ; }
        public string Name { get; }
        public MeetingSchedule Schedule { get; }
        public MeetingPlace Place { get; }
        public List<Participant> Participants { get; }
    }
}