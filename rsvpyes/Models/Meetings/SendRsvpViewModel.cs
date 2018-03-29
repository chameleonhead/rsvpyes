using rsvpyes.Data;
using System.Collections.Generic;

namespace rsvpyes.Models.Meetings
{
    public sealed class SendRsvpViewModel
    {
        public Meeting Meeting { get; set; }
        public ICollection<User> Users { get; set; }
        public string DefaultTitle { get; set; }
        public string DefaultMessage { get; set; }
    }
}
