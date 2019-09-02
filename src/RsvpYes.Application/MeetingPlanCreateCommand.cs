using RsvpYes.Domain;
using System;

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

    }
}