using MediatR;
using RsvpYes.Domain.Meetings;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdatePlacesCommandHandler :
        IRequestHandler<MeetingPlanUpdatePlacesCommand>
    {
        private readonly IMeetingPlanRepository _repository;

        public MeetingPlanUpdatePlacesCommandHandler(IMeetingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(MeetingPlanUpdatePlacesCommand command, CancellationToken cancellationToken = default)
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
            return Unit.Value;
        }
    }
}
