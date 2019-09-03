using Microsoft.EntityFrameworkCore;
using RsvpYes.Data.MeetingPlans;
using RsvpYes.Domain;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Data
{
    public class MeetingPlanRepository : IMeetingPlanRepository
    {
        private readonly RsvpYesDbContext _context;

        public MeetingPlanRepository(RsvpYesDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(MeetingPlan meetingPlan)
        {
            var meetingId = meetingPlan.Id.Value;
            var meetingPlanEntity = await _context.MeetingPlans.FindAsync(meetingId).ConfigureAwait(false);
            var meetingPlanParticipantEntities = await _context.MeetingPlanParticipants.Where(m => m.MeetingId == meetingId).ToListAsync().ConfigureAwait(false);
            var meetingPlanPlaceCandidateEntities = await _context.MeetingPlanPlaceCandidates.Where(m => m.MeetingId == meetingId).ToListAsync().ConfigureAwait(false);
            var meetingPlanScheduleCandidateEntities = await _context.MeetingPlanScheduleCandidates.Where(m => m.MeetingId == meetingId).ToListAsync().ConfigureAwait(false);

            if (meetingPlanEntity != null)
            {
                _context.MeetingPlans.Remove(meetingPlanEntity);
                _context.MeetingPlanParticipants.RemoveRange(meetingPlanParticipantEntities);
                _context.MeetingPlanPlaceCandidates.RemoveRange(meetingPlanPlaceCandidateEntities);
                _context.MeetingPlanScheduleCandidates.RemoveRange(meetingPlanScheduleCandidateEntities);
            }

            var participants = meetingPlan.Participants.ToList();
            var placeCandidates = meetingPlan.PlaceCandidates.ToList();
            var scheduleCandidates = meetingPlan.ScheduleCandidates.ToList();

            _context.MeetingPlans.Add(new MeetingPlanEntity()
            {
                Id = meetingId,
                HostedBy = meetingPlan.HostedBy.Value,
                Name = meetingPlan.Name,
                CreatedAt = meetingPlan.CreatedAt,
                IsFixed = meetingPlan.IsFixed,
                PlaceCandidateSeq = meetingPlan.Place == null ? default(int?) : placeCandidates.FindIndex(c => c.Place.Equals(meetingPlan.Place)),
                ScheduleCandidateSeq = meetingPlan.Schedule == null ? default(int?) : scheduleCandidates.FindIndex(c => c.Schedule.Equals(meetingPlan.Schedule)),
            });
            _context.MeetingPlanParticipants.AddRange(
                participants.Select(p => new MeetingPlanParticipantEntity()
                {
                    MeetingId = meetingId,
                    UserId = p.UserId.Value,
                    Role = (MeetingPlanParticipantRole)(int)p.Role
                })
            );
            _context.MeetingPlanPlaceCandidates.AddRange(
                placeCandidates.Select(p => new MeetingPlanPlaceCandidateEntity()
                {
                    MeetingId = meetingId,
                    Seq = placeCandidates.IndexOf(p),
                    PlaceId = p.Place.PlaceId.Value,
                    DetailInfo = p.Place.DetailInfo
                })
            );
            _context.MeetingPlanScheduleCandidates.AddRange(
                scheduleCandidates.Select(p => new MeetingPlanScheduleCandidateEntity()
                {
                    MeetingId = meetingId,
                    Seq = scheduleCandidates.IndexOf(p),
                    BeginAt = p.Schedule.BeginAt,
                    Duration = p.Schedule.Duration
                })
            );
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<MeetingPlan> FindByIdAsync(MeetingId meetingId)
        {
            var meetingPlanEntity = await _context.MeetingPlans.FindAsync(meetingId.Value).ConfigureAwait(false);
            var meetingPlanParticipantEntities = await _context.MeetingPlanParticipants.Where(m => m.MeetingId == meetingId.Value).ToListAsync().ConfigureAwait(false);
            var meetingPlanPlaceCandidateEntities = await _context.MeetingPlanPlaceCandidates.Where(m => m.MeetingId == meetingId.Value).ToListAsync().ConfigureAwait(false);
            var meetingPlanScheduleCandidateEntities = await _context.MeetingPlanScheduleCandidates.Where(m => m.MeetingId == meetingId.Value).ToListAsync().ConfigureAwait(false);

            var participants = meetingPlanParticipantEntities.Select(p => new Participant((ParticipantRole)(int)p.Role, new UserId(p.UserId)));
            var placeCandidates = meetingPlanPlaceCandidateEntities.Select(p => new MeetingPlaceCandidate(new MeetingPlace(new PlaceId(p.PlaceId), p.DetailInfo)));
            var scheduleCandidates = meetingPlanScheduleCandidateEntities.Select(p => new MeetingScheduleCandidate(new MeetingSchedule(p.BeginAt, p.Duration)));

            var meetingPlan = new MeetingPlan(
                meetingId,
                new UserId(meetingPlanEntity.HostedBy),
                meetingPlanEntity.Name,
                meetingPlanEntity.CreatedAt,
                meetingPlanEntity.PlaceCandidateSeq.HasValue
                    ? placeCandidates.Skip(meetingPlanEntity.PlaceCandidateSeq.Value).First().Place
                    : null,
                meetingPlanEntity.ScheduleCandidateSeq.HasValue
                    ? scheduleCandidates.Skip(meetingPlanEntity.ScheduleCandidateSeq.Value).First().Schedule
                    : null,
                meetingPlanEntity.IsFixed,
                participants,
                placeCandidates,
                scheduleCandidates);
            return meetingPlan;
        }
    }
}
