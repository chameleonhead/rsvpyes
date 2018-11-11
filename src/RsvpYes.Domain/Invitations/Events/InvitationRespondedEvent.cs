using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Invitations
{
    public class InvitationRespondedEvent : DomainEvent<Invitation>
    {
        public bool Participate { get; }
        public string Remarks { get; }

        public InvitationRespondedEvent(Invitation entity, bool participate, string remarks) : base(entity)
        {
            Participate = participate;
            Remarks = remarks;
        }
    }
}