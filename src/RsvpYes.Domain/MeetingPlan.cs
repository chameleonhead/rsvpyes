using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(host != null);
            Contract.Requires<ArgumentNullException>(name != null);

            Id = id;
            HostedBy = host;
            Name = name;
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
            Contract.Requires(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires(!_participants.Any(p => p.UserId.Equals(userId)), Constants.ParticipantAlreadyExists);
            _participants.Add(new Participant(ParticipantRole.MainGuest, userId));
        }

        public void AddGuest(UserId userId)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(!_participants.Any(p => p.UserId.Equals(userId)), Constants.ParticipantAlreadyExists);
            _participants.Add(new Participant(ParticipantRole.Guest, userId));
        }

        public void RemoveParticipant(UserId participantUserId)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(_participants.Any(p => p.UserId.Equals(participantUserId)), Constants.ParticipantNotExistsError);
            var participant = _participants.First(p => p.UserId.Equals(participantUserId));
            _participants.Remove(participant);
        }

        public void AddCandidateSchedule(MeetingSchedule schedule)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(!_meetingScheduleCandidates.Any(c => c.Schedule.Equals(schedule)), Constants.CandidateScheduleAlreadyExistsError);
            _meetingScheduleCandidates.Add(new MeetingScheduleCandidate(Id, schedule));
            FixScheduleIfCandidateIsOnlyOne();
        }

        public void RemoveCandidateSchedule(MeetingSchedule schedule)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(_meetingScheduleCandidates.Any(c => c.Schedule.Equals(schedule)), Constants.CandidateScheduleNotExistsError);
            var candidate = _meetingScheduleCandidates.First(c => c.Schedule.Equals(schedule));
            _meetingScheduleCandidates.Remove(candidate);
            FixScheduleIfCandidateIsOnlyOne();
        }

        private void FixScheduleIfCandidateIsOnlyOne()
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            if (_meetingScheduleCandidates.Count == 1)
            {
                Schedule = _meetingScheduleCandidates[0].Schedule;
            }
            else
            {
                Schedule = null;
            }
        }

        public void SelectCandidateSchedule(MeetingSchedule schedule)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(_meetingScheduleCandidates.Any(c => c.Schedule.Equals(schedule)), Constants.CandidateScheduleNotExistsError);
            Schedule = schedule;
        }

        public void AddCandidatePlace(MeetingPlace place)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(!_meetingPlaceCandidates.Any(c => c.Place.Equals(place)), Constants.CandidatePlaceAlreadyExistsError);
            _meetingPlaceCandidates.Add(new MeetingPlaceCandidate(Id, place));
            FixPlaceIfCandidateIsOnlyOne();
        }

        public void RemoveCandidatePlace(MeetingPlace place)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(_meetingPlaceCandidates.Any(c => c.Place.Equals(place)), Constants.CandidatePlaceNotExistsError);
            var candidate = _meetingPlaceCandidates.First(c => c.Place.Equals(place));
            _meetingPlaceCandidates.Remove(candidate);
            FixPlaceIfCandidateIsOnlyOne();
        }

        private void FixPlaceIfCandidateIsOnlyOne()
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            if (_meetingPlaceCandidates.Count == 1)
            {
                Place = _meetingPlaceCandidates[0].Place;
            }
            else
            {
                Place = null;
            }
        }

        public void SelectCandidatePlace(MeetingPlace place)
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(_meetingPlaceCandidates.Any(c => c.Place.Equals(place)), Constants.CandidatePlaceNotExistsError);
            Place = place;
        }

        public Meeting Fix()
        {
            Contract.Requires<InvalidOperationException>(!IsFixed, Constants.MeetingPlanAlreadyFixed);
            Contract.Requires<InvalidOperationException>(IsScheduleFixed && IsPlaceFixed, Constants.FixScheduleAndPlaceFirst);
            IsFixed = true;
            return new Meeting(Id, Name, Schedule, Place, _participants);
        }
    }
}
