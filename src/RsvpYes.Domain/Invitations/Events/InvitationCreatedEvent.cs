using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.SeedWork;
using System;

namespace RsvpYes.Domain.Invitations.Events
{
    public class InvitationCreatedEvent : DomainEvent<Invitation>
    {
        public Guid InvitationId { get; }
        public DateTime Created { get; }
        public Meeting Meeting { get; }
        public Participant Participant { get; }

        public InvitationCreatedEvent(Invitation entity, Guid invitationId, DateTime created, Meeting meeting, Participant participant) : base(entity)
        {
            InvitationId = invitationId;
            Created = created;
            Meeting = meeting;
            Participant = participant;
        }
    }
}
