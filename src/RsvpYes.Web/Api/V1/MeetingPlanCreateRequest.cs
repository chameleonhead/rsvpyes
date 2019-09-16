using System;

namespace RsvpYes.Web.Api.V1
{
    public class MeetingPlanCreateRequest
    {
        public string MeetingName { get; set; }
        public string PlaceName { get; set; }
        public DateTime? BeginAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
