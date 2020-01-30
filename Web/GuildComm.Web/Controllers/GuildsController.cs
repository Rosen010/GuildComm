namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
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

            return this.View(guilds);
        }
    }
}
