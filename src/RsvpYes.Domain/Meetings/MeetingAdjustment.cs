using RsvpYes.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace RsvpYes.Domain.Meetings
{
    public class MeetingAdjustment<T>
    {
        private readonly List<UserId> _participants;
        private readonly List<T> _selections;

        public MeetingAdjustment(
            MeetingId meetingId, 
            IEnumerable<UserId> participants, 
            IEnumerable<T> selections)
        {
            _participants = new List<UserId>();
            _selections = new List<T>();

            Id = new MeetingAdjustmentId();
            ForMeetingId = meetingId;
            _participants.AddRange(participants);
            _selections.AddRange(selections);
        }

        public MeetingAdjustmentId Id { get; }
        public MeetingId ForMeetingId { get; }
        public IEnumerable<UserId> Participants => _participants.ToList();
        public IEnumerable<T> Selections => _selections.ToList();
    }
}
