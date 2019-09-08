using MediatR;
using RsvpYes.Domain.Meetings;
using System.Threading;
using System.Threading.Tasks;

namespace RsvpYes.Application.Meetings
{
    public class MeetingPlanCreateCommandHandler : IRequestHandler<MeetingPlanCreateCommand, MeetingId>
    {
        private readonly IMeetingPlanRepository _repository;

        public MeetingPlanCreateCommandHandler(IMeetingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<MeetingId> Handle(MeetingPlanCreateCommand request, CancellationToken cancellationToken = default)
        {
            var meetingPlan = new MeetingPlan(request.Host, request.MeetingName, request.Timestamp);
            foreach (var userId in request.MainGuests)
            {
                meetingPlan.AddMainGuest(userId);
            }
            foreach (var userId in request.Guests)
            {
                meetingPlan.AddGuest(userId);
            }
            foreach (var place in request.Places)
            {
                meetingPlan.AddCandidatePlace(place);
            }
            foreach (var schedule in request.Schedules)
            {
                meetingPlan.AddCandidateSchedule(schedule);
            }
            await _repository.SaveAsync(meetingPlan).ConfigureAwait(false);
            return meetingPlan.Id;
        }
    }
}
