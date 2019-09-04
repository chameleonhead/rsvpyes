namespace RsvpYes.Domain.Users
{
    public class Organization
    {
        public Organization(string name)
        {
            Id = new OrganizationId();
            Name = name;
        }

        public Organization(
            OrganizationId organizationId, 
            string name)
        {
            Id = organizationId;
            Name = name;
        }

        public OrganizationId Id { get; }
        public string Name { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}