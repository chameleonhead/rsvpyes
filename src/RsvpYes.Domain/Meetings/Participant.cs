using RsvpYes.Domain.Users;
using System.Collections.Generic;

namespace RsvpYes.Domain.Meetings
{
    public class Participant
    {
        public Participant(ParticipantRole role, UserId userId)
        {
            Role = role;
            UserId = userId;
        }

        public ParticipantRole Role { get; }
        public UserId UserId { get; }

        public override bool Equals(object obj)
        {
            return obj is Participant participant &&
                   Role == participant.Role &&
                   EqualityComparer<UserId>.Default.Equals(UserId, participant.UserId);
        }

        public override int GetHashCode()
        {
            var hashCode = 1852434784;
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<UserId>.Default.GetHashCode(UserId);
            return hashCode;
        }
    }
}