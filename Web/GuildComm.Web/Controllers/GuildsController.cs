using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Guild;
using Microsoft.AspNetCore.Mvc;

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
                return this.View(guild);
            }

            return this.Redirect("/Home/Error");
        }
    }
}
