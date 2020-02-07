namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class GuildsController : Controller
    {
        private readonly IRealmsService realmsService;
        private readonly IGuildsService guildsService;
        private readonly ICharactersService charactersService;
        private readonly IUsersService usersService;

        public GuildsController(IRealmsService realmsService, 
            IGuildsService guildsService, 
            ICharactersService charactersService,
            IUsersService usersService)
        {
            this.realmsService = realmsService;
            this.guildsService = guildsService;
            this.charactersService = charactersService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.usersService.GetUserAsync();
            GuildCreateInputModel bindingModel = new GuildCreateInputModel();

            bindingModel.Realms = await this.realmsService.GetAllRealmViewModelsAsync();
            bindingModel.Characters = await this.charactersService.GetUserCharactersAsync(user);
          
            return this.View(bindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuildCreateInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {     
                await this.guildsService.CreateGuildAsync(inputModel);
            }

            return this.RedirectToAction("All", "Guilds");
        }

        public async Task<IActionResult> Details(string id)
        {
            var guild = await this.guildsService.GetGuildByIdAsync(id);

            return this.View(guild);
        }

        public async Task<IActionResult> All()
        {
            var guilds = await guildsService.GetAllGuildsAsync();

            return this.View(guilds);
        }
    }
}
