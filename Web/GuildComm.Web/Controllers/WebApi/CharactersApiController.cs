using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Character;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers.WebApi
{
    public class CharactersApiController : Controller
    {
        private readonly IRealmService _realmService;

        public CharactersApiController(IRealmService realmService)
        {
            _realmService = realmService;
        }

        [HttpGet]
        public async Task<IActionResult> CharacterForm(CharacterInputModel model)
        {
            var viewModel = new CharacterInputModel();
            viewModel.Realms = await _realmService.GetRealmsByRegionAsync(Localizations.Regions.EU);

            return this.PartialView("~/Views/Home/CharacterForm.cshtml", viewModel);
        }
    }
}
