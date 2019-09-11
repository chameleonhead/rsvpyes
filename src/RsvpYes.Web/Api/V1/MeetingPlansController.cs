using MediatR;
using Microsoft.AspNetCore.Mvc;
using RsvpYes.Application.Meetings;
using RsvpYes.Query;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Web.Api.V1
{
    [Route("api/v1/meetingplans")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMeetingsQuery _query;

        public MeetingsController(IMeetingsQuery query, IMediator mediator)
        {
            _mediator = mediator;
            _query = query;
        }

        [HttpGet]
        public async Task<ActionResult<MeetingPlanSummariesResponse>> Get()
        {
            var plans = await _query.FetchMeetingPlansAsync().ConfigureAwait(false);
            return Ok(new MeetingPlanSummariesResponse() { Meetings = plans.ToList() });
        }

        [HttpPost]
        public async Task<ActionResult> Post(MeetingPlanCreateCommand command)
        {
            var id = await _mediator.Send(command).ConfigureAwait(false);
            return Created($"/api/v1/meetingplans/{id}", null);
        }
    }
}
