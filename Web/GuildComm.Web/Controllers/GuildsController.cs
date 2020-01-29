namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class GuildsController : Controller
    {
        private readonly IRealmsService realmsService;
        private readonly IGuildsService guildsService;

        public GuildsController(IRealmsService realmsService, IGuildsService guildsService)
        {
            this.realmsService = realmsService;
            this.guildsService = guildsService;
        }

        public async Task<IActionResult> Create()
        {
            CreateGuildBindingModel bindingModel = new CreateGuildBindingModel();

            bindingModel.Realms = await this.realmsService.GetAllRealmsAsync();
          
            return this.View(bindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGuildBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                Guild guild = new Guild
                {
                    Name = bindingModel.Name,
                    Realm = await this.realmsService.GetRealmAsync(bindingModel.Realm)
                };

                await this.guildsService.CreateGuildAsync(guild);
            }

            return this.RedirectToAction("All", "Guilds");
        }

        public async Task<IActionResult> All()
        {
            var guilds = await guildsService.GetAllGuildsAsync();

            var guildsViewModel = guilds.Select(g => new GuildsAllViewModel
            {
                Name = g.Name,
                Realm = g.Realm,
                MembersCount = g.Members.Count()
            })
            .ToList();

            return this.View(guildsViewModel);
        }
    }
}
