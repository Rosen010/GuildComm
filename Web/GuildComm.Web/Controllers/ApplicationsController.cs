namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ApplicationsController : Controller
    {
        private readonly IGuildsService guildsService;

        public ApplicationsController(IGuildsService guildsService)
        {
            this.guildsService = guildsService;
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
    }
}
