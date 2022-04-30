using GuildComm.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class RealmsController : Controller
    {
        private readonly IRealmService _realmService;

        public RealmsController(IRealmService realmService)
        {
            _realmService = realmService;
        }

        [HttpGet]
        public async Task<JsonResult> UpdateRealms(string region)
        {
            var realms = await _realmService.GetRealmsByRegionAsync(region);
            return this.Json(realms);
        }
    }
}
