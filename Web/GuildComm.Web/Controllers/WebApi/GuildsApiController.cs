using GuildComm.Common;
using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Guild;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace GuildComm.Web.Controllers.WebApi
{
    public class GuildsApiController : Controller
    {
        private readonly IRealmService _realmService;

        public GuildsApiController(IRealmService realmService)
        {
            _realmService = realmService;
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
