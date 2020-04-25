namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Applications;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ApplicationsController : Controller
    {
        private readonly IGuildsService guildsService;
        private readonly IApplicationsService applicationsService;

        public ApplicationsController(IGuildsService guildsService, IApplicationsService applicationsService)
        {
            this.guildsService = guildsService;
            this.applicationsService = applicationsService;
        }

        [Authorize]
        public async Task<IActionResult> Apply(string id)
        {
            if (await this.guildsService.IsUserInTargetGuild(id))
            {
                return this.Redirect("/Home/Error");
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var inputModel = new ApplicationCreateInputModel
            {
                GuildId = id
            };

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Apply(ApplicationCreateInputModel inputModel)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            if (this.ModelState.IsValid)
            {
                await this.applicationsService.CreateApplicationAsync(inputModel);

                return this.Redirect("/Guilds/All");
            }

            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var application = await this.applicationsService.GetApplicationByIdAsync(id);

            if (!await this.guildsService.IsUserAuthorized(application.GuildId))
            {
                return this.Redirect("/Guilds/All");
            }

            return this.View(application);
        }

        [Authorize]
        public async Task<IActionResult> All(string id)
        {
            if (!await this.guildsService.IsUserAuthorized(id))
            {
                return this.Redirect("/Guilds/All");
            }

            var applicationModels = await this.applicationsService.GetAllGuildApplications(id);

            return this.View(applicationModels);
        }

        public async Task<IActionResult> Dismiss(int id)
        {
            await this.applicationsService.Dismiss(id);

            return this.Redirect("/Applications/All");
        }
    }
}
