using System;

namespace RsvpYes.Data.Users
{
    class IdentityEntity
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string PasswordHash { get; set; }
        public Guid? UserId { get; set; }
    }
}
