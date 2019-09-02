using System.Threading.Tasks;

namespace RsvpYes.Domain
{
    public interface IOrganizationRepository
    {
        Task<Organization> FindByIdAsync(OrganizationId organizationId);
        Task SaveAsync(Organization organization);
    }
}
