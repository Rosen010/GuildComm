namespace GuildComm.Web.Controllers
{
    using GuildComm.Services.Contracts;
    using GuildComm.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IActionResult Create(string id)
        {
            EventCreateInputModel inputModel = new EventCreateInputModel
            {
                GuildId = id
            };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventCreateInputModel inputModel)
        {
            await this.eventsService.CreateEvent(inputModel);
            return this.Redirect($"/Guilds/Manage/{inputModel.GuildId}");
        }

        public async Task<IActionResult> All(string id)
        {
            var events = await this.eventsService.GetGuildEvents(id);
            return this.View(events);
        }
    }
}
