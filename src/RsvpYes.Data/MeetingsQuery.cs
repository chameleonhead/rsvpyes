using Microsoft.EntityFrameworkCore;
using RsvpYes.Query;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<MeetingPlanSummary>> FetchMeetingPlansAsync()
        {
            var places = await _context.Places.ToDictionaryAsync(p => p.Id).ConfigureAwait(false);
            var meetingPlans = await _context.MeetingPlans
                .Include(m => m.Participants)
                .Include(m => m.PlaceCandidates)
                .Include(m => m.ScheduleCandidates)
                .ToListAsync().ConfigureAwait(false);
            return meetingPlans
                .Select(m =>
                {
                    var place = m.PlaceCandidates.FirstOrDefault(p => p.Seq == m.PlaceCandidateSeq);
                    var schedule = m.ScheduleCandidates.FirstOrDefault(p => p.Seq == m.ScheduleCandidateSeq);
                    return new MeetingPlanSummary()
                    {
                        Id = m.Id.ToString(),
                        Name = m.Name,
                        BeginAt = schedule?.BeginAt,
                        Duration = schedule?.Duration,
                        Place = place != null ? places.TryGetValue(place.PlaceId, out var pl) ? new Place() { Id = pl.Id, Name = pl.Name } : null : null
                    };
                });
        }
    }
}
