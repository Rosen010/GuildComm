using GuildComm.Common;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Guild;

using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class GuildsController : Controller
    {
        private readonly IGuildService _guildService;

        public GuildsController(IGuildService guildService)
        {
            _guildService = guildService;
        }

        public async Task<IActionResult> Guild(GuildInputModel model)
        {
            if (ModelState.IsValid)
            {
                var guild = await _guildService.FindGuiildAsync(model);

                if (guild != null)
                {
                    return this.View(guild);
                }

                return this.Redirect(string.Format(GlobalConstants.ErrorPage, HttpStatusCode.NotFound));
            }

            return this.Redirect(string.Format(GlobalConstants.ErrorPage, HttpStatusCode.InternalServerError));
        }
    }
}
