using Microsoft.AspNetCore.Mvc;
using rsvpyes.Data;
using rsvpyes.Models.Messages;
using rsvpyes.Services;
using System;
using System.Threading.Tasks;

namespace rsvpyes.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IDataService<Message> messagesDataService;
        private readonly IDataService<User> usersDataService;

        public MessagesController(
            IDataService<Message> messagesDataService, 
            IDataService<User> usersDataService)
        {
            this.messagesDataService = messagesDataService;
            this.usersDataService = usersDataService;
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = (await messagesDataService.Find(id));
            if (message == null)
            {
                return NotFound();
            }

            var user = (await usersDataService.Find(message.SenderUserId));
            if (user == null)
            {
                return NotFound();
            }

            return View(new MessageViewModel()
            {
                SenderName = user.Name + (string.IsNullOrEmpty(user.Organization) ? "" : $" ({user.Organization})"),
                SendTimestamp = message.SendTimestamp,
                Title = message.Title,
                Body = message.Body
            });
        }
    }
}
