using Microsoft.AspNetCore.Mvc;
using rsvpyes.Data;
using rsvpyes.Models;
using rsvpyes.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rsvpyes.Controllers
{
    public class ResponseController : Controller
    {
        IDataService<User> userService;
        IDataService<Meeting> meetingService;
        IDataService<RsvpRequest> rsvpRequestService;
        IDataService<RsvpResponse> rsvpResponseService;

        public ResponseController(
            IDataService<User> userService,
            IDataService<Meeting> meetingService,
            IDataService<RsvpRequest> rsvpRequestService,
            IDataService<RsvpResponse> rsvpResponseService)
        {
            this.userService = userService;
            this.meetingService = meetingService;
            this.rsvpRequestService = rsvpRequestService;
            this.rsvpResponseService = rsvpResponseService;
        }

        public async Task<IActionResult> Respond(Guid id)
        {
            var rsvpRequest = (await rsvpRequestService.Where(u => u.Id == id)).FirstOrDefault();
            if (rsvpRequest == null)
            {
                return NotFound();
            }

            var meeting = (await meetingService.Where(u => u.Id == rsvpRequest.MeetingId)).FirstOrDefault();
            if (meeting == null)
            {
                return NotFound();
            }

            var user = (await userService.Where(u => u.Id == rsvpRequest.UserId)).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            return View(new RespondViewModel()
            {
                Id = id,
                MeetingName = meeting.Name,
                StartTime = meeting.StartTime,
                UserName = user.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> RespondYes(Guid id)
        {
            await rsvpResponseService.Insert(new RsvpResponse()
            {
                RsvpRequestId = id,
                Rsvp = Rsvp.Yes,
                Timestamp = DateTime.Now,
            });
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> RespondNo(Guid id, [FromForm] string reason)
        {
            await rsvpResponseService.Insert(new RsvpResponse()
            {
                RsvpRequestId = id,
                Rsvp = Rsvp.No,
                Reason = reason,
                Timestamp = DateTime.Now,
            });
            return Redirect("/");
        }
    }
}