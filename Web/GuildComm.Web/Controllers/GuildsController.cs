namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels;
    using GuildComm.Web.ViewModels.Guild;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using GuildComm.Web.ViewModels.Characters;

    public class GuildsController : Controller
    {
        private readonly IRealmsService realmsService;
        private readonly IGuildsService guildsService;
        private readonly ICharactersService charactersService;
        private readonly IUsersService usersService;
        private readonly IApplicationsService applicationsService;

        public GuildsController(IRealmsService realmsService, 
            IGuildsService guildsService, 
            ICharactersService charactersService,
            IUsersService usersService,
            IApplicationsService applicationsService)
        {
            this.realmsService = realmsService;
            this.guildsService = guildsService;
            this.charactersService = charactersService;
            this.usersService = usersService;
            this.applicationsService = applicationsService;
        }

        public async Task<IActionResult> Create()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var user = await this.usersService.GetUserAsync();
            GuildCreateInputModel inputModel = new GuildCreateInputModel();

            inputModel.Realms = await this.realmsService.GetAllRealmViewModelsAsync();
            inputModel.Characters = await this.charactersService.GetUserCharactersViewModelAsync<CharacterViewModel>();
          
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuildCreateInputModel inputModel)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            if (this.ModelState.IsValid)
            {     
                await this.guildsService.CreateGuildAsync(inputModel);
                return this.RedirectToAction("All", "Guilds");
            }

            return this.View(inputModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var guild = await this.guildsService.GetGuildViewModelByIdAsync<GuildDetailsViewModel>(id);

            return this.View(guild);
        }

        public async Task<IActionResult> All()
        {
            var guilds = await guildsService.GetAllGuildsAsync();

            return this.View(guilds);
        }

        public async Task<IActionResult> Disband(string id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            if (!this.User.IsInRole("Admin"))
            {
                return this.Redirect("/Guilds/All");
            }

            await this.guildsService.RemoveGuildAsync(id);

            return this.RedirectToAction("All", "Guilds");
        }

        public async Task<IActionResult> Manage(string id)
        {
            if (!await this.guildsService.IsUserAuthorized(id))
            {
                return this.Redirect("/Guilds/All");
            }

            var guildModel = await this.guildsService.GetGuildViewModelByIdAsync<GuildManageViewModel>(id);

            return this.View(guildModel);
        }

        public async Task<IActionResult> AddMember(int id)
        {
            var application = await this.applicationsService.GetApplicationByIdAsync(id);

            if (!await this.guildsService.IsUserAuthorized(application.GuildId))
            {
                return this.Redirect("/Guilds/All");
            }

            await this.guildsService.AddMemberAsync(application.CharacterId, "Trial", application.GuildId);
            await this.applicationsService.Dismiss(id);

            return this.Redirect("/Guilds/All");
        }
    }
}