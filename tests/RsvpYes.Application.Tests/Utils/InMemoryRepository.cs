using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests.Utils
{
    public class InMemoryRepository<TIdentity, T>
        where T : class
    {
        private readonly Dictionary<TIdentity, T> _store = new Dictionary<TIdentity, T>();
        private readonly Func<T, TIdentity> _func;

        public InMemoryRepository(Func<T, TIdentity> func)
        {
            _func = func;
        }

        public Task<T> FindByIdAsync(TIdentity meetingId)
        {
            return Task.FromResult(_store.TryGetValue(meetingId, out var value) ? value : null);
        }

        public Task SaveAsync(T meetingPlan)
        {
            _store[_func(meetingPlan)] = meetingPlan;
            return Task.CompletedTask;
        }
    }

    public class InMemoryMeetingPlanRepository
        : InMemoryRepository<MeetingId, MeetingPlan>, IMeetingPlanRepository
    {
        public InMemoryMeetingPlanRepository()
            : base(e => e.Id)
        {
        }
    }

    public class InMemoryPlaceRepository
        : InMemoryRepository<PlaceId, Place>, IPlaceRepository
    {
        public InMemoryPlaceRepository()
            : base(e => e.Id)
        {
        }
    }

    public class InMemoryUserRepository
        : InMemoryRepository<UserId, User>, IUserRepository
    {
        public InMemoryUserRepository()
            : base(e => e.Id)
        {
        }
    }
}
