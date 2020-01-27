namespace GuildComm.Web.Controllers
{
    using GuildComm.Data.Models;
    using GuildComm.Services;
    using GuildComm.Web.Models.Guild;
    using Microsoft.AspNetCore.Mvc;

    public class GuildsController : Controller
    {
        private readonly IRealmsService realmsService;
        private readonly IGuildsService guildsService;

        public GuildsController(IRealmsService realmsService, IGuildsService guildsService)
        {
            this.realmsService = realmsService;
            this.guildsService = guildsService;
        }

        public IActionResult Create()
        {
            this.ViewData["Realms"] = this.realmsService.GetAllRealms();

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateGuildBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                Guild guild = new Guild
                {
                    Name = bindingModel.Name,
                    Realm = this.realmsService.GetRealm(bindingModel.Realm)
                };

                this.guildsService.CreateGuild(guild);
            }

            this.ViewData["Realms"] = this.realmsService.GetAllRealms();
            return this.View();
        }
    }
}
