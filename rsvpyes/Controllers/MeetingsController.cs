using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rsvpyes.Data;
using rsvpyes.Models.Meetings;
using rsvpyes.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rsvpyes.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IDataService<Meeting> meetingsService;
        private readonly IDataService<User> usersService;
        private readonly IDataService<RsvpRequest> rsvpRequestsService;
        private readonly IDataService<RsvpResponse> rsvpResponsesService;
        private readonly IMailService mailService;

        public MeetingsController(
            IDataService<Meeting> meetingsService,
            IDataService<User> usersService,
            IDataService<RsvpRequest> rsvpRequestsService,
            IDataService<RsvpResponse> rsvpResponsesService,
            IMailService mailService)
        {
            this.meetingsService = meetingsService;
            this.usersService = usersService;
            this.rsvpRequestsService = rsvpRequestsService;
            this.rsvpResponsesService = rsvpResponsesService;
            this.mailService = mailService;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            return View(await meetingsService.Where(u => true));
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await meetingsService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            var requests = await rsvpRequestsService.Where(r => r.MeetingId == id);
            var status = await Task.WhenAll(requests.Select(async req =>
            {
                var response = (await rsvpResponsesService.Where(res => res.RsvpRequestId == req.Id)).OrderByDescending(res => res.Timestamp).FirstOrDefault();
                return new MeetingInvitationResponseStatus()
                {
                    User = await usersService.Find(req.UserId),
                    RequestId = req.Id,
                    RsvpResponse = new Response()
                    {
                        Rsvp = (response?.Rsvp) ?? Rsvp.NotRespond,
                        Reason = response?.Reason
                    },
                };
            }));

            return View(new MeetingDetailsViewModel()
            {
                Meeting = meeting,
                ResponseStatus = status
            });
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meetings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                meeting.Id = Guid.NewGuid();
                await meetingsService.Insert(meeting);
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await meetingsService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await meetingsService.Update(meeting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await MeetingExists(meeting.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await meetingsService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var meeting = await meetingsService.Find(id);
            var requests = await rsvpRequestsService.Where(r => r.MeetingId == id);
            foreach (var request in requests)
            {
                var responses = await rsvpResponsesService.Where(res => res.RsvpRequestId == request.Id);
                foreach (var response in responses)
                {
                    await rsvpResponsesService.Remove(response);
                }
                await rsvpRequestsService.Remove(request);
            }
            await meetingsService.Remove(meeting);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MeetingExists(Guid id)
        {
            return (await meetingsService.Where(e => e.Id == id)).Any();
        }

        public async Task<IActionResult> SendRsvp(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await meetingsService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(new SendRsvpViewModel()
            {
                Meeting = meeting,
                Users = (await usersService.Where(u => true)).OrderBy(u => u.Name).ToList(),
                DefaultTitle = $"{meeting.Name}への誘い",
                DefaultMessage = $@"お疲れ様です。
下記の通り{meeting.Name}を開催したいと存じます。
お忙しいところ恐縮ですが、出欠のご連絡をいただきたいと存じます。
よろしくお願いいたします。

        記

日時
{meeting.StartTime.ToString("M/d (ddd) HH:mm")} ～
 
場所
{meeting.PlaceName}{(string.IsNullOrEmpty(meeting.PlaceUri) ? "" : Environment.NewLine + meeting.PlaceUri)}
 
会費
{meeting.Fee?.ToString("#,#0円")}
"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendRsvp(Guid id, [FromForm] MailSendCommand command)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await meetingsService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            command.ResponseUri = Request.Scheme + "://" + Request.Host.Value + Url.Action("respond", "response");
            await mailService.Send(command);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInvitation(Guid id, [FromForm] Guid requestId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await meetingsService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            var request = await rsvpRequestsService.Find(requestId);
            if (request == null)
            {
                return NotFound();
            }

            if (request.MeetingId != id)
            {
                return BadRequest();
            }

            await Task.WhenAll((await rsvpResponsesService.Where(r => r.RsvpRequestId == requestId))
                .Select(async r => await rsvpResponsesService.Remove(r)));
            await rsvpRequestsService.Remove(request);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
