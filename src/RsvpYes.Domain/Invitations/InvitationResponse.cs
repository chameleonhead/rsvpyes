using RsvpYes.Domain.SeedWork;

namespace RsvpYes.Domain.Invitations
{
    public class InvitationResponse : Entity
    {
        public bool Participate { get; private set; }
        public string Remarks { get; private set; }

        public InvitationResponse(bool participate, string remarks)
        {
            Participate = true;
            Remarks = remarks;
        }
    }
}