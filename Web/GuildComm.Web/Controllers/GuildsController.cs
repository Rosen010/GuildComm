namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;

    public class GuildsController : Controller
    {
        private readonly IRealmsService realmsService;
        private readonly IGuildsService guildsService;

        private readonly IMapper mapper;

        public GuildsController(IRealmsService realmsService, IGuildsService guildsService, IMapper mapper)
        {
            this.realmsService = realmsService;
            this.guildsService = guildsService;

            this.mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            GuildCreateInputModel bindingModel = new GuildCreateInputModel();

            bindingModel.Realms = await this.realmsService.GetAllRealmsAsync();
          
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
