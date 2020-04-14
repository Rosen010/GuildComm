namespace GuildComm.Web.Areas.Administration.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Guild;

    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IGuildsService guildsService;
        private const int ItemsPerPage = 5;

        public AdministrationController(IGuildsService guildsService)
        {
            this.guildsService = guildsService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new GuildsListingModel();
            model.Guilds = await guildsService.GetAllGuildsAsync(ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.guildsService.GetGuildsCount();
            model.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = page;

            return this.View(model);
        }

        public async Task<IActionResult> Disband(string id)
        {
            await this.guildsService.RemoveGuildAsync(id);

            return this.RedirectToAction("Index", "Administration");
        }
    }
}
