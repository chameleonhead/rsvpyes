using System;
using System.Collections.Generic;
using RsvpYes.Domain.Meetings.Events;
using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Meetings
{
    public class Meeting : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public MeetingTime Time { get; set; }
        public MeetingPlace Place { get; set; }
        public MeetingDetails Details { get; set; }
        public List<Participant> Invitations { get; } = new List<Participant>();

        public Meeting(string name, MeetingTime time, MeetingPlace place, MeetingDetails details)
        {
            OnMeetingCreated(name, time, place, details);
        }

        public void ChangeTime(MeetingTime newTime)
        {
            OnMeetingTimeChanged(newTime, Time);
        }

        private void OnMeetingCreated(string name, MeetingTime time, MeetingPlace place, MeetingDetails details)
        {
            Name = name;
            Time = time;
            Place = place;
            Details = details;

            var ev = new MeetingCreatedEvent(this, name, time, place, details);
            AddDomainEvent(ev);
        }

        public void OnMeetingTimeChanged(MeetingTime newTime, MeetingTime oldTime)
        {
            Time = newTime;

            var ev = new MeetingTimeChanged(this, newTime, oldTime);
            AddDomainEvent(ev);
        }

        public void OnMeetingTimeChanged(MeetingPlace newPlace, MeetingPlace oldPlace)
        {
            Place = newPlace;

            var ev = new MeetingPlaceChanged(this, newPlace, oldPlace);
            AddDomainEvent(ev);
        }
    }
}
