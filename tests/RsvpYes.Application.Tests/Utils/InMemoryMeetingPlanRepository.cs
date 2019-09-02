using RsvpYes.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests.Utils
{
    public class InMemoryMeetingPlanRepository : IMeetingPlanRepository
    {
        private Dictionary<MeetingId, MeetingPlan> _store = new Dictionary<MeetingId, MeetingPlan>();

        public Task<MeetingPlan> FindByIdAsync(MeetingId meetingId)
        {
            return Task.FromResult(_store.TryGetValue(meetingId, out var value) ? value : null);
        }

        public Task SaveAsync(MeetingPlan meetingPlan)
        {
            _store[meetingPlan.Id] = meetingPlan;
            return Task.CompletedTask;
        }
    }
}
