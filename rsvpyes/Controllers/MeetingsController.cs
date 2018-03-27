using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rsvpyes.Data;
using rsvpyes.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rsvpyes.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IDataService<Meeting> dataService;

        public MeetingsController(IDataService<Meeting> dataService)
        {
            this.dataService = dataService;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            return View(await dataService.Where(u => true));
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await dataService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
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
                await dataService.Insert(meeting);
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

            var meeting = await dataService.Find(id);
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
                    await dataService.Update(meeting);
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

            var meeting = await dataService.Find(id);
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
            var meeting = await dataService.Find(id);
            await dataService.Remove(meeting);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MeetingExists(Guid id)
        {
            return (await dataService.Where(e => e.Id == id)).Any();
        }

        public async Task<IActionResult> SendRsvp(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await dataService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendRsvp(Guid id, [FromForm] MailSendCommand command)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await dataService.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
