namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Applications;
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

        public async Task<IActionResult> Apply(string id)
        {
            if (await this.guildsService.IsUserInTargetGuild(id))
            {
                return this.Unauthorized("You are already a member of this guild");
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
        public async Task<IActionResult> Apply(ApplicationCreateInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.applicationsService.CreateApplicationAsync(inputModel);

                return this.Redirect("/Guilds/All");
            }

            return this.View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var application = await this.applicationsService.GetApplicationByIdAsync(id);

            return this.View(application);
        }

        public async Task<IActionResult> All(string id)
        {
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
