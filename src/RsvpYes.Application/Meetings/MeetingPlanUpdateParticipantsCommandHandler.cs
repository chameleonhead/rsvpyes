using MediatR;
using RsvpYes.Domain.Meetings;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdateParticipantsCommandHandler :
        IRequestHandler<MeetingPlanUpdateParticipantsCommand>
    {
        private readonly IMeetingPlanRepository _repository;

        public MeetingPlanUpdateParticipantsCommandHandler(IMeetingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(MeetingPlanUpdateParticipantsCommand command, CancellationToken cancellationToken = default)
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
            return Unit.Value;
        }
    }
}
