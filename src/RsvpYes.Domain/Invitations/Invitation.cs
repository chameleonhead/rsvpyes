using System;
using RsvpYes.Domain.Invitations.Events;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Invitations
{
    public class Invitation : Entity, IAggregateRoot
    {
        public Guid InvitationId { get; set; }
        public DateTime Created { get; set; }
        public Meeting Meeting { get; set; }
        public Participant Participant { get; set; }
        public Message Message { get; }

        protected Invitation() { }

        public Invitation(Meeting meeting, Participant participant)
        {
            OnInvitationCreated(Guid.NewGuid(), DateTime.UtcNow, meeting, participant);
        }

        public void SendInvitation(Message message)
        {
            OnInvitationSent(message);
        }

        public void RespondToAttend(string remarks = null)
        {
            OnParticipatntRespondedToInvitation(true, remarks);
        }

        public void RespondNotToAttend(string remarks)
        {
            OnParticipatntRespondedToInvitation(false, remarks);
        }

        private void OnInvitationCreated(Guid invitationId, DateTime created, Meeting meeting, Participant participant)
        {
            InvitationId = InvitationId;
            Created = created;
            Meeting = meeting;
            Participant = participant;

            var ev = new InvitationCreatedEvent(this, invitationId, created, meeting, participant);
            AddDomainEvent(ev);
        }

        private void OnInvitationSent(Message message)
        {
            throw new NotImplementedException();
        }

        private void OnParticipatntRespondedToInvitation(bool attend, string remarks)
        {
            throw new NotImplementedException();
        }
    }
}
