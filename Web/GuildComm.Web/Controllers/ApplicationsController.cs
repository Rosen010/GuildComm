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

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplicationCreateInputModel inputModel)
        {
            await this.applicationsService.CreateApplicationAsync(inputModel);

            return this.Redirect("/Guilds/All");
        }

        public async Task<IActionResult> All(string guildId)
        {
            var applicationModels = await this.applicationsService.GetAllGuildApplications(guildId);

            return this.View(applicationModels);
        }
    }
}
