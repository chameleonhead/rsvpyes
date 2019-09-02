using System;
using System.Collections.Generic;
using System.Linq;

namespace RsvpYes.Domain
{
    public class MeetingPlan
    {
        private readonly List<Participant> _participants;
        private readonly List<MeetingScheduleCandidate> _meetingScheduleCandidates;
        private readonly List<MeetingPlaceCandidate> _meetingPlaceCandidates;

        public MeetingPlan(MeetingId id, UserId host, string name, DateTime createdAt)
        {
            Id = id;
            HostedBy = host ?? throw new ArgumentNullException(nameof(host));
            Name = name ?? throw new ArgumentNullException(nameof(host));
            CreatedAt = createdAt;
            _participants = new List<Participant>();
            _participants.Add(new Participant(ParticipantRole.Host, host));
            _meetingScheduleCandidates = new List<MeetingScheduleCandidate>();
            _meetingPlaceCandidates = new List<MeetingPlaceCandidate>();
        }

        public MeetingId Id { get; }
        public UserId HostedBy { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; }
        public MeetingSchedule Schedule { get; private set; }
        public bool IsScheduleFixed => Schedule != null;
        public MeetingPlace Place { get; private set; }
        public bool IsPlaceFixed => Place != null;

        public IEnumerable<Participant> Participants => _participants;
        public IEnumerable<MeetingScheduleCandidate> ScheduleCandidates => _meetingScheduleCandidates;
        public IEnumerable<MeetingPlaceCandidate> PlaceCandidates => _meetingPlaceCandidates;

        public bool IsFixed { get; private set; }

        public void AddMainGuest(UserId userId)
        {
            ThrowIfMeetingPlanFixed();
            if (_participants.Any(p => p.UserId.Equals(userId)))
            {
                throw new InvalidOperationException(Constants.ParticipantAlreadyExists);
            }
            _participants.Add(new Participant(ParticipantRole.MainGuest, userId));
        }

        public void AddGuest(UserId userId)
        {
            ThrowIfMeetingPlanFixed();
            if (_participants.Any(p => p.UserId.Equals(userId)))
            {
                throw new InvalidOperationException(Constants.ParticipantAlreadyExists);
            }
            _participants.Add(new Participant(ParticipantRole.Guest, userId));
        }

        public void RemoveParticipant(UserId participantUserId)
        {
            ThrowIfMeetingPlanFixed();
            if (!_participants.Any(p => p.UserId.Equals(participantUserId)))
            {
                throw new InvalidOperationException(Constants.ParticipantNotExistsError);
            }
            var participant = _participants.First(p => p.UserId.Equals(participantUserId));
            _participants.Remove(participant);
        }

        public void AddCandidateSchedule(MeetingSchedule schedule)
        {
            ThrowIfMeetingPlanFixed();
            if (_meetingScheduleCandidates.Any(c => c.Schedule.Equals(schedule)))
            {
                throw new InvalidOperationException(Constants.CandidateScheduleAlreadyExistsError);
            }
            _meetingScheduleCandidates.Add(new MeetingScheduleCandidate(Id, schedule));
            FixScheduleIfCandidateIsOnlyOne();
        }

        public void RemoveCandidateSchedule(MeetingSchedule schedule)
        {
            ThrowIfMeetingPlanFixed();
            if (!_meetingScheduleCandidates.Any(c => c.Schedule.Equals(schedule)))
            {
                throw new InvalidOperationException(Constants.CandidateScheduleNotExistsError);
            }
            var candidate = _meetingScheduleCandidates.First(c => c.Schedule.Equals(schedule));
            _meetingScheduleCandidates.Remove(candidate);
            FixScheduleIfCandidateIsOnlyOne();
        }

        public void SelectCandidateSchedule(MeetingSchedule schedule)
        {
            ThrowIfMeetingPlanFixed();
            if (!_meetingScheduleCandidates.Any(c => c.Schedule.Equals(schedule)))
            {
                throw new InvalidOperationException(Constants.CandidateScheduleNotExistsError);
            }
            Schedule = schedule;
        }

        public void AddCandidatePlace(MeetingPlace place)
        {
            ThrowIfMeetingPlanFixed();
            if (_meetingPlaceCandidates.Any(c => c.Place.Equals(place)))
            {
                throw new InvalidOperationException(Constants.CandidatePlaceAlreadyExistsError);
            }
            _meetingPlaceCandidates.Add(new MeetingPlaceCandidate(Id, place));
            FixPlaceIfCandidateIsOnlyOne();
        }

        public void RemoveCandidatePlace(MeetingPlace place)
        {
            ThrowIfMeetingPlanFixed();
            if (!_meetingPlaceCandidates.Any(c => c.Place.Equals(place)))
            {
                throw new InvalidOperationException(Constants.CandidatePlaceNotExistsError);
            }
            var candidate = _meetingPlaceCandidates.First(c => c.Place.Equals(place));
            _meetingPlaceCandidates.Remove(candidate);
            FixPlaceIfCandidateIsOnlyOne();
        }

        public void SelectCandidatePlace(MeetingPlace place)
        {
            ThrowIfMeetingPlanFixed();
            if (!_meetingPlaceCandidates.Any(c => c.Place.Equals(place)))
            {
                throw new InvalidOperationException(Constants.CandidatePlaceNotExistsError);
            }
            Place = place;
        }

        public Meeting Fix()
        {
            ThrowIfMeetingPlanFixed();
            if (!IsScheduleFixed || !IsPlaceFixed)
            {
                throw new InvalidOperationException(Constants.FixScheduleAndPlaceFirst);
            }
            IsFixed = true;
            return new Meeting(Id, Name, Schedule, Place, _participants);
        }

        private void ThrowIfMeetingPlanFixed()
        {
            if (IsFixed)
            {
                throw new InvalidOperationException(Constants.MeetingPlanAlreadyFixed);
            }
        }

        private void FixScheduleIfCandidateIsOnlyOne()
        {
            if (_meetingScheduleCandidates.Count == 1)
            {
                Schedule = _meetingScheduleCandidates[0].Schedule;
            }
            else
            {
                Schedule = null;
            }
        }

        private void FixPlaceIfCandidateIsOnlyOne()
        {
            ThrowIfMeetingPlanFixed();
            if (_meetingPlaceCandidates.Count == 1)
            {
                Place = _meetingPlaceCandidates[0].Place;
            }
            else
            {
                Place = null;
            }
        }
    }
}
