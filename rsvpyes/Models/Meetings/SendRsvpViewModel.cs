using rsvpyes.Data;
using System.Collections.Generic;

namespace rsvpyes.Models.Meetings
{
    public sealed class SendRsvpViewModel
    {
        public Meeting Meeting { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
