using System;
using System.Collections.Generic;

namespace RsvpYes.Data.Meetings
{
    internal class MeetingPlanEntity
    {
        public Guid Id { get; set; }
        public Guid HostedBy { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ScheduleCandidateSeq { get; set; }
        public int? PlaceCandidateSeq { get; set; }
        public bool IsFixed { get; set; }
        public ICollection<MeetingPlanParticipantEntity> Participants { get; } = new List<MeetingPlanParticipantEntity>();
        public ICollection<MeetingPlanScheduleCandidateEntity> ScheduleCandidates { get; } = new List<MeetingPlanScheduleCandidateEntity>();
        public ICollection<MeetingPlanPlaceCandidateEntity> PlaceCandidates { get; } = new List<MeetingPlanPlaceCandidateEntity>();
    }
}
