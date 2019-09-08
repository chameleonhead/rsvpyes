using MediatR;
using RsvpYes.Domain.Meetings;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdateSchedulesCommandHandler :
        IRequestHandler<MeetingPlanUpdateSchedulesCommand>
    {
        private readonly IMeetingPlanRepository _repository;

        public MeetingPlanUpdateSchedulesCommandHandler(IMeetingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(MeetingPlanUpdateSchedulesCommand command, CancellationToken cancellationToken = default)
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
            return Unit.Value;
        }
    }
}
