using rsvpyes.Data;
using System.Collections.Generic;

namespace rsvpyes.Models.Meetings
{
    public sealed class MeetingInvitationResponseStatus
    {
        public User User { get; set; }
        public Rsvp RsvpResponse { get; set; }
    }

    public sealed class MeetingDetailsViewModel
    {
        public Meeting Meeting { get; set; }
        public ICollection<MeetingInvitationResponseStatus> ResponseStatus { get; set; }
    }
}
