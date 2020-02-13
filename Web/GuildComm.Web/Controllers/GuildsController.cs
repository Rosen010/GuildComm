﻿namespace GuildComm.Web.Controllers
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
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var user = await this.usersService.GetUserAsync();
            GuildCreateInputModel inputModel = new GuildCreateInputModel();

            inputModel.Realms = await this.realmsService.GetAllRealmViewModelsAsync();
            inputModel.Characters = await this.charactersService.GetUserCharactersViewModelAsync();
          
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

            var guild = await this.guildsService.GetGuildViewModelByIdAsync(id);

            return this.View(guild);
        }

        public async Task<IActionResult> All()
        {
            var guilds = await guildsService.GetAllGuildsAsync();

            return this.View(guilds);
        }

        //[HttpPost]
        //public async Task<IActionResult> Disband(string id)
        //{

        //}
    }
}
