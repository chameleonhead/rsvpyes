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
        public async Task<IActionResult> Index([FromQuery]string organization)
        {
            var allUsers = (await dataService.Where(u => true))
                .OrderBy(e => e.Organization)
                .ThenBy(e => e.Name);
            var users = allUsers
                .Where(u => string.IsNullOrEmpty(organization) || u.Organization == organization)
                .ToList();
            var organizations = allUsers
                .Where(u => !string.IsNullOrEmpty(u.Organization))
                .Select(u => u.Organization)
                .Distinct()
                .ToList();
            ViewData["Organization"] = organization;
            ViewData["Organizations"] = organizations;
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id, [FromQuery]string organization)
        {
            ViewData["Organization"] = organization;
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
        public IActionResult Create([FromQuery]string organization)
        {
            ViewData["Organization"] = organization;
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] User user, [FromQuery]string organization)
        {
            ViewData["Organization"] = organization;
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await dataService.Insert(user);
                return RedirectToIndex(organization);
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id, [FromQuery]string organization)
        {
            ViewData["Organization"] = organization;
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
        public async Task<IActionResult> Edit(Guid id, [FromQuery]string organization, [FromForm] User user)
        {
            ViewData["Organization"] = organization;
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
                return RedirectToIndex(organization);
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id, [FromQuery]string organization)
        {
            ViewData["Organization"] = organization;
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
        public async Task<IActionResult> DeleteConfirmed(Guid id, [FromQuery]string organization)
        {
            ViewData["Organization"] = organization;
            var user = await dataService.Find(id);
            await dataService.Remove(user);
            return RedirectToIndex(organization);
        }

        private async Task<bool> UserExists(Guid id)
        {
            return (await dataService.Where(e => e.Id == id)).Any();
        }

        private IActionResult RedirectToIndex(string organization)
        {
            if (string.IsNullOrEmpty(organization))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index), new { organization });
            }
        }
    }
}
