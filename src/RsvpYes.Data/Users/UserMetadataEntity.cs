using System;

namespace RsvpYes.Data.Users
{
    internal class UserMetadataEntity
    {
        public Guid UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
