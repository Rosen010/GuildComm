using GuildComm.Common;
using GuildComm.Common.Constants;
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
        private readonly IRealmService _realmService;

        public GuildsController(IGuildService guildService, IRealmService realmService)
        {
            _guildService = guildService;
            _realmService = realmService;
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

        [HttpGet]
        public async Task<IActionResult> GuildForm()
        {
            var viewModel = new GuildInputModel();
            viewModel.Realms = await _realmService.GetRealmsByRegionAsync(Localizations.Regions.EU);

            return this.PartialView("~/Views/Home/GuildForm.cshtml", viewModel);
        }
    }
}
