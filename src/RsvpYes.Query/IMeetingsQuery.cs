using System.Collections.Generic;
using System.Threading.Tasks;

namespace RsvpYes.Query
{
    public interface IMeetingsQuery
    {
        Task<IEnumerable<MeetingPlanSummary>> FetchMeetingPlansAsync();
    }
}
