using System;

namespace RsvpYes.Query
{
    public class MeetingPlanSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Place Place { get; set; }
        public DateTime? BeginAt { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? EndAt => BeginAt.HasValue && Duration.HasValue ? BeginAt + Duration : default;
    }
}
