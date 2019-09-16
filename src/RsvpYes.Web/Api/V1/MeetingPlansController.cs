using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RsvpYes.Application.Meetings;
using RsvpYes.Query;
using System.Linq;
using System.Threading.Tasks;

namespace RsvpYes.Web.Api.V1
{
    [Route("api/v1/meetingplans")]
    [ApiController]
    [Authorize]
    [EnableCors]
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
        [Produces("application/json", Type = typeof(MeetingPlanSummariesResponse))]
        public async Task<ActionResult<MeetingPlanSummariesResponse>> Get()
        {
            var plans = await _query.FetchMeetingPlansAsync().ConfigureAwait(false);
            return Ok(new MeetingPlanSummariesResponse() { Meetings = plans.ToList() });
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult> Post([FromForm] MeetingPlanCreateRequest request)
        {
            var command = new MeetingPlanCreateCommand(request.MeetingName, new Domain.Users.UserId());
            var id = await _mediator.Send(command).ConfigureAwait(false);
            return Created($"/api/v1/meetingplans/{id}", null);
        }
    }
}
