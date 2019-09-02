namespace RsvpYes.Domain
{
    public class Organization
    {
        public Organization(string name)
        {
            Id = new OrganizationId();
            Name = name;
        }

        public OrganizationId Id { get; }
        public string Name { get; }
    }
}