using RsvpYes.Data.Users;
using RsvpYes.Domain.Users;
using System;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly RsvpYesDbContext _context;

        public OrganizationRepository(RsvpYesDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync(Organization organization)
        {
            var organizationId = organization.Id.Value;
            var organizationEntity = await _context.Organizations.FindAsync(organizationId).ConfigureAwait(false);

            if (organizationEntity != null)
            {
                _context.Organizations.Remove(organizationEntity);
            }

            _context.Organizations.Add(new OrganizationEntity()
            {
                Id = organizationId,
                Name = organization.Name
            });
        }

        public async Task<Organization> FindByIdAsync(OrganizationId organizationId)
        {
            var organizationEntity = await _context.Organizations.FindAsync(organizationId.Value).ConfigureAwait(false);
            if (organizationEntity == null)
            {
                return null;
            }
            return new Organization(organizationId, organizationEntity.Name);
        }
    }
}
