using RsvpYes.Query;
using System.Collections.Generic;

namespace RsvpYes.Web.Api.V1
{
    public class MeetingPlanSummariesResponse
    {
        public List<MeetingPlanSummary> Meetings { get; set; }
    }
}
