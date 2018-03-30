using rsvpyes.Data;
using System;
using System.Collections.Generic;

namespace rsvpyes.Models.Response
{
    public sealed class Response
    {
        public Rsvp Rsvp { get; set; }
        public override string ToString()
        {
            switch (Rsvp)
            {
                case Rsvp.Yes:
                    return "参加";
                case Rsvp.No:
                    return "不参加";
                default:
                    return "未回答";
            }
        }
    }

    public sealed class ResponseStatusForOthers
    {
        public Guid RequestId { get; set; }
        public User User { get; set; }
        public Response RsvpResponse { get; set; }
    }

    public sealed class ResponseForOthersViewModel
    {
        public Guid Id { get; set; }
        public Meeting Meeting { get; set; }
        public ICollection<ResponseStatusForOthers> Responses { get; set; }
    }
}
