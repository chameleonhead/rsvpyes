using System.Threading.Tasks;

namespace RsvpYes.Domain.Users
{
    public interface IOrganizationRepository
    {
        Task<Organization> FindByIdAsync(OrganizationId organizationId);
        Task SaveAsync(Organization organization);
    }
}
