namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels;
    using GuildComm.Web.ViewModels.Characters;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

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

        public async Task<IActionResult> All()
        {
            var guilds = await guildsService.GetAllGuildsAsync();

            return this.View(guilds);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            GuildCreateInputModel inputModel = new GuildCreateInputModel();

            inputModel.Realms = await this.realmsService.GetAllRealmViewModelsAsync();
            inputModel.Characters = await this.charactersService.GetUserCharactersAsync<CharacterViewModel>();

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GuildCreateInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {     
                await this.guildsService.CreateGuildAsync(inputModel);
                return this.RedirectToAction("All", "Guilds");
            }

            return this.View(inputModel);
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var guild = await this.guildsService.GetGuildViewModelByIdAsync(id);

            return this.View(guild);
        }

        [Authorize]
        public async Task<IActionResult> Promote(string id, string guildId)
        {

            if (!await this.guildsService.IsUserAuthorized(id))
            {
                return this.Redirect("/Guilds/All");
            }

            await this.guildsService.PromoteMemberAsync(id);
            return this.RedirectToAction("Manage", "Guilds", new { id = guildId });
        }

        [Authorize]
        public async Task<IActionResult> Demote(string id, string guildId)
        {

            if (!await this.guildsService.IsUserAuthorized(id))
            {
                return this.Redirect("/Guilds/All");
            }

            await this.guildsService.DemoteMemberAsync(id);
            return this.RedirectToAction("Manage", "Guilds", new { id = guildId });
        }

        [Authorize]
        public async Task<IActionResult> Manage(string id)
        {
            if (!await this.guildsService.IsUserAuthorized(id))
            {
                return this.Redirect("/Guilds/All");
            }

            var guildModel = await this.guildsService.GetGuildManageViewModelByIdAsync(id);

            return this.View(guildModel);
        }

        [Authorize]
        public async Task<IActionResult> RemoveMember(string id)
        {

            if (!await this.guildsService.IsUserAuthorized(id))
            {
                return this.Redirect("/Guilds/All");
            }

            await this.guildsService.RemoveMemberAsync(id);
            return this.Redirect("/Guilds/All");
        }
    }
}