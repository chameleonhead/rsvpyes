using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Users;
using System;
using System.Collections.Generic;

namespace RsvpYes.Application
{
    public class MeetingPlanCreateCommand
    {
        public MeetingPlanCreateCommand(string meetingName, UserId host)
        {
            Timestamp = DateTime.UtcNow;
            MeetingName = meetingName;
            Host = host;
        }

        public DateTime Timestamp { get; }
        public string MeetingName { get; }
        public UserId Host { get; }
        public List<UserId> MainGuests { get; } = new List<UserId>();
        public List<UserId> Guests { get; } = new List<UserId>();
        public List<MeetingPlace> Places { get; } = new List<MeetingPlace>();
        public List<MeetingSchedule> Schedules { get; } = new List<MeetingSchedule>();
    }
}