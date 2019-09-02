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
            foreach (var userId in command.MainGuests)
            {
                meetingPlan.AddMainGuest(userId);
            }
            foreach (var userId in command.Guests)
            {
                meetingPlan.AddGuest(userId);
            }
            foreach (var place in command.Places)
            {
                meetingPlan.AddCandidatePlace(place);
            }
            foreach (var schedule in command.Schedules)
            {
                meetingPlan.AddCandidateSchedule(schedule);
            }
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
            return meetingPlan.Id;
        }

        public async Task UpdateAsync(MeetingPlanUpdateMeetingNameCommand command)
        {
            var meetingPlan = await _repository.FindByIdAsync(command.MeetingId).ConfigureAwait(false);
            meetingPlan.UpdateName(command.MeetingName);
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
        }

        public async Task UpdateAsync(MeetingPlanUpdateParticipantsCommand command)
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

        public async Task UpdateAsync(MeetingPlanUpdateSchedulesCommand command)
        {
            var meetingPlan = await _repository.FindByIdAsync(command.MeetingId).ConfigureAwait(false);
            foreach (var schedule in command.SchedulesToRemove)
            {
                meetingPlan.RemoveCandidateSchedule(schedule);
            }
            foreach (var schedule in command.SchedulesToAdd)
            {
                meetingPlan.AddCandidateSchedule(schedule);
            }
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
        }

        public async Task UpdateAsync(MeetingPlanUpdatePlacesCommand command)
        {
            var meetingPlan = await _repository.FindByIdAsync(command.MeetingId).ConfigureAwait(false);
            foreach (var place in command.PlacesToRemove)
            {
                meetingPlan.RemoveCandidatePlace(place);
            }
            foreach (var place in command.PlacesToAdd)
            {
                meetingPlan.AddCandidatePlace(place);
            }
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
        }
    }
}
