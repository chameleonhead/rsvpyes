using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Application.Tests.Utils
{
    public class InMemoryRepository<TIdentity, T>
        where T : class
    {
        protected readonly Dictionary<TIdentity, T> _store = new Dictionary<TIdentity, T>();
        private readonly Func<T, TIdentity> _func;

        public InMemoryRepository(Func<T, TIdentity> func)
        {
            _func = func;
        }

        public Task<T> FindByIdAsync(TIdentity id)
        {
            return Task.FromResult(_store.TryGetValue(id, out var value) ? value : null);
        }

        public Task SaveAsync(T data)
        {
            _store[_func(data)] = data;
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

    public class InMemoryIdentityRepository
        : InMemoryRepository<IdentityId, Identity>, IIdentityRepository
    {
        public InMemoryIdentityRepository()
            : base(e => e.Id)
        {
        }

        public Task<Identity> FindByAccountNameAndPasswordAsync(string accountName, string passwordHash)
        {
            return Task.FromResult(_store.Values.SingleOrDefault(v => v.AccountName == accountName && v.PasswordHash == passwordHash));
        }
    }

    public class InMemoryOrganizationRepository
        : InMemoryRepository<OrganizationId, Organization>, IOrganizationRepository
    {
        public InMemoryOrganizationRepository()
            : base(e => e.Id)
        {
        }

        public Task<Organization> FindByNameAsync(string organizationName)
        {
            return Task.FromResult(_store.Values.FirstOrDefault(v => v.Name == organizationName));
        }
    }
}
