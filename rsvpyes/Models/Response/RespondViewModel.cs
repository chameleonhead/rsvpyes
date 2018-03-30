using rsvpyes.Data;
using System;

namespace rsvpyes.Models.Response
{
    public sealed class RespondViewModel
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Meeting Meeting { get; set; }
    }
}
