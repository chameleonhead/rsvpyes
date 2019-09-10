using RsvpYes.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class MeetingsQuery : IMeetingsQuery
    {
        private readonly RsvpYesDbContext _context;

        public MeetingsQuery(RsvpYesDbContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<MeetingPlanSummary>> FetchMeetingPlansAsync()
        {
            throw new NotImplementedException();
        }
    }
}
