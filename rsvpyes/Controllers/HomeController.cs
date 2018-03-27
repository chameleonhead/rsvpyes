using Microsoft.AspNetCore.Mvc;
using rsvpyes.Models;
using System.Diagnostics;

namespace rsvpyes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "回答ありがとうございます。";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
