namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Services.Contracts;
    using GuildComm.Web.ViewModels.Characters;
    using GuildComm.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly ICharactersService charactersService;
        private readonly IGuildsService guildsService;

        public EventsController(IEventsService eventsService, 
            ICharactersService charactersService,
            IGuildsService guildsService)
        {
            this.eventsService = eventsService;
            this.charactersService = charactersService;
            this.guildsService = guildsService;
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

        public async Task<IActionResult> SignUp(int id, string guildId)
        {
            var userCharacters = await this.charactersService.GetUserCharactersAsync<CharacterViewModel>();

            var inputModel = new EventSignUpInputModel
            {
                Characters = userCharacters,
                EventId = id,
                GuildId = guildId
            };
            
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(EventSignUpInputModel inputModel)
        {
            var characters = await this.charactersService.GetCharactersByNameAsync<CharacterViewModel>(inputModel.Character);
            var guild = await this.guildsService.GetGuildViewModelByIdAsync(inputModel.GuildId);

            var character = characters.Where(c => c.RealmName == guild.RealmName).FirstOrDefault();

            await this.eventsService.AddMemberToEventAsync(character.Id, inputModel.EventId);
            return this.RedirectToAction("All", "Events", new { id = inputModel.GuildId });
        }
    }
}
