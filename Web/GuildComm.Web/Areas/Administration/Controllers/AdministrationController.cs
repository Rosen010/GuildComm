namespace GuildComm.Web.Areas.Administration.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IGuildsService guildsService;

        public AdministrationController(IGuildsService guildsService)
        {
            this.guildsService = guildsService;
        }

        public async Task<IActionResult> Index()
        {
            var guilds = await this.guildsService.GetAllGuildsAsync();

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
    }
}
