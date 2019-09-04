using System;

namespace RsvpYes.Data.Meetings
{
    class MeetingPlanPlaceCandidateEntity
    {
        public Guid MeetingId { get; set; }
        public int Seq { get; set; }
        public Guid PlaceId { get; set; }
        public string DetailInfo { get; set; }
    }
}
