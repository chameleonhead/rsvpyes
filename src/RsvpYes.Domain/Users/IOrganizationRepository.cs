using System.Threading.Tasks;

namespace RsvpYes.Domain.Users
{
    public interface IOrganizationRepository
    {
        Task<Organization> FindByIdAsync(OrganizationId organizationId);
        Task<Organization> FindByNameAsync(string organizationName);
        Task SaveAsync(Organization organization);
    }
}
