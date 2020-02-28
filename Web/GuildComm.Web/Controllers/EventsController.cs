namespace GuildComm.Web.Controllers
{
    using GuildComm.Services.Contracts;
    using GuildComm.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
