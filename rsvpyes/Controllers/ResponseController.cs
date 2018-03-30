using Microsoft.AspNetCore.Mvc;
using rsvpyes.Data;
using rsvpyes.Models.Response;
using rsvpyes.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rsvpyes.Controllers
{
    public class ResponseController : Controller
    {
        IDataService<User> usersService;
        IDataService<Meeting> meetingsService;
        IDataService<RsvpRequest> rsvpRequestsService;
        IDataService<RsvpResponse> rsvpResponsesService;

        public ResponseController(
            IDataService<User> userService,
            IDataService<Meeting> meetingService,
            IDataService<RsvpRequest> rsvpRequestService,
            IDataService<RsvpResponse> rsvpResponseService)
        {
            this.usersService = userService;
            this.meetingsService = meetingService;
            this.rsvpRequestsService = rsvpRequestService;
            this.rsvpResponsesService = rsvpResponseService;
        }

        public async Task<IActionResult> ResponsesForOthers(Guid id)
        {
            var rsvpRequest = (await rsvpRequestsService.Where(u => u.Id == id)).FirstOrDefault();
            if (rsvpRequest == null)
            {
                return NotFound();
            }

            var meeting = (await meetingsService.Where(u => u.Id == rsvpRequest.MeetingId)).FirstOrDefault();
            if (meeting == null)
            {
                return NotFound();
            }

            var user = (await usersService.Where(u => u.Id == rsvpRequest.UserId)).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            var requests = await rsvpRequestsService.Where(r => r.MeetingId == meeting.Id);
            var status = await Task.WhenAll(requests.Select(async req =>
            {
                var response = (await rsvpResponsesService.Where(res => res.RsvpRequestId == req.Id)).OrderByDescending(res => res.Timestamp).FirstOrDefault();
                return new ResponseStatusForOthers()
                {
                    User = await usersService.Find(req.UserId),
                    RequestId = req.Id,
                    RsvpResponse = new Response()
                    {
                        Rsvp = (response?.Rsvp) ?? Rsvp.NotRespond,
                    },
                };
            }));

            return View(new ResponseForOthersViewModel()
            {
                Id = id,
                Meeting = meeting,
                Responses = status
                    .OrderBy(o => o.RsvpResponse.Rsvp == Rsvp.Yes ? 0 : o.RsvpResponse.Rsvp == Rsvp.No ? 1 : 2)
                    .ThenBy(o => o.User.Name)
                    .ToList(),
            });
        }

        public async Task<IActionResult> Respond(Guid id)
        {
            var rsvpRequest = (await rsvpRequestsService.Where(u => u.Id == id)).FirstOrDefault();
            if (rsvpRequest == null)
            {
                return NotFound();
            }

            var meeting = (await meetingsService.Where(u => u.Id == rsvpRequest.MeetingId)).FirstOrDefault();
            if (meeting == null)
            {
                return NotFound();
            }

            var user = (await usersService.Where(u => u.Id == rsvpRequest.UserId)).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            return View(new RespondViewModel()
            {
                Id = id,
                Meeting = meeting,
                User = user,
            });
        }

        [HttpPost]
        public async Task<IActionResult> RespondYes(Guid id)
        {
            await rsvpResponsesService.Insert(new RsvpResponse()
            {
                RsvpRequestId = id,
                Rsvp = Rsvp.Yes,
                Timestamp = DateTime.Now,
            });
            return RedirectToAction(nameof(ThankYou));
        }

        [HttpPost]
        public async Task<IActionResult> RespondNo(Guid id, [FromForm] string reason)
        {
            await rsvpResponsesService.Insert(new RsvpResponse()
            {
                RsvpRequestId = id,
                Rsvp = Rsvp.No,
                Reason = reason,
                Timestamp = DateTime.Now,
            });
            return RedirectToAction(nameof(ThankYou));
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}