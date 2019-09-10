using System;

namespace RsvpYes.Query
{
    public class MeetingPlanSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Place Place { get; set; }
        public DateTime? BeginAt { get; set; }
        public DateTime? EndAt { get; set; }
        public TimeSpan? Duration => BeginAt.HasValue && EndAt.HasValue ? EndAt - BeginAt : default;
    }
}
