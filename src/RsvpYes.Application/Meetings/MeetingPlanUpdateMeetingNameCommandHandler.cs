using MediatR;
using RsvpYes.Domain.Meetings;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanUpdateMeetingNameCommandHandler :
        IRequestHandler<MeetingPlanUpdateMeetingNameCommand>
    {
        private readonly IMeetingPlanRepository _repository;

        public MeetingPlanUpdateMeetingNameCommandHandler(IMeetingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(MeetingPlanUpdateMeetingNameCommand command, CancellationToken cancellationToken = default)
        {
            var meetingPlan = await _repository.FindByIdAsync(command.MeetingId).ConfigureAwait(false);
            meetingPlan.UpdateName(command.MeetingName);
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
