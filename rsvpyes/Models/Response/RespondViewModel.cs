using System;

namespace rsvpyes.Models.Response
{
    public sealed class RespondViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string MeetingName { get; internal set; }
        public DateTime StartTime { get; internal set; }
    }
}
