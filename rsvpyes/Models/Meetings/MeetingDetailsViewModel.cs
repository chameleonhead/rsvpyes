using rsvpyes.Data;
using System;
using System.Collections.Generic;

namespace rsvpyes.Models.Meetings
{
    public sealed class Response
    {
        public Rsvp Rsvp { get; set; }
        public string Reason { get; set; }
        public override string ToString()
        {
            switch (Rsvp) {
                case Rsvp.Yes:
                    return "参加";
                case Rsvp.No:
                    return $"不参加 ( {Reason} )";
                default:
                    return "未回答";
            }
        }
    }

    public sealed class MeetingInvitationResponseStatus
    {
        public Guid RequestId { get; set; }
        public Guid? MessageId { get; set; }
        public User User { get; set; }
        public Response RsvpResponse { get; set; }
    }

    public sealed class MeetingDetailsViewModel
    {
        public Meeting Meeting { get; set; }
        public ICollection<MeetingInvitationResponseStatus> ResponseStatus { get; set; }
    }
}
