using System;

namespace RsvpYes.Data.Users
{
    internal class UserEntity
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string DefaultMailAddress { get; set; }
    }
}
