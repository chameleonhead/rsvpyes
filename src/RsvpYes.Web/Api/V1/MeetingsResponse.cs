using RsvpYes.Query;
using System.Collections.Generic;

namespace RsvpYes.Web.Api.V1
{
    public class MeetingsResponse
    {
        public List<MeetingPlanSummary> Meetings { get; set; }
    }
}
