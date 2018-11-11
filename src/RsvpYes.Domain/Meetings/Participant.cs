using RsvpYes.Domain.SeedWork;
using System;

namespace RsvpYes.Domain.Meetings
{
    public class Participant : Entity
    {
        public Guid UserId { get; set; }
        public RsvpResponse Response { get; set; }
    }
}
