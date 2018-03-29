using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rsvpyes.Data;
using rsvpyes.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rsvpyes.Controllers
{
    public class UsersController : Controller
    {
        private readonly IDataService<User> dataService;

        public UsersController(IDataService<User> dataService)
        {
            this.dataService = dataService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await dataService.Where(u => true));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = (await dataService.Find(id));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await dataService.Insert(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await dataService.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await dataService.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await UserExists(user.Id)))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await dataService.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await dataService.Find(id);
            await dataService.Remove(user);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(Guid id)
        {
            return (await dataService.Where(e => e.Id == id)).Any();
        }
    }
}
