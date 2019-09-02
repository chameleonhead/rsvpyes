using RsvpYes.Domain;
using System.Threading.Tasks;

namespace RsvpYes.Application
{
    public class MeetingPlanService
    {
        private readonly IMeetingPlanRepository _repository;

        public MeetingPlanService(IMeetingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<MeetingId> CreateAsync(MeetingPlanCreateCommand command)
        {
            var meetingPlan = new MeetingPlan(command.Host, command.MeetingName, command.Timestamp);
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
            return meetingPlan.Id;
        }

        public async Task UpdateAsync(MeetingPlanUpdateCommand command)
        {
            var meetingPlan = await _repository.FindByIdAsync(command.MeetingId).ConfigureAwait(false);
            meetingPlan.UpdateName(command.MeetingName);
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
        }

        public async Task UpdateParticipantsAsync(MeetingPlanUpdateParticipantsGuest command)
        {
            var meetingPlan = await _repository.FindByIdAsync(command.MeetingId).ConfigureAwait(false);
            foreach (var userId in command.ParticipantsToRemove)
            {
                meetingPlan.RemoveParticipant(userId);
            }
            foreach (var userId in command.MainGuests)
            {
                meetingPlan.AddMainGuest(userId);
            }
            foreach (var userId in command.Guests)
            {
                meetingPlan.AddGuest(userId);
            }
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
        }
    }
}
