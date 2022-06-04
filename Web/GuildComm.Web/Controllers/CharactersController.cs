using GuildComm.Common;
using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Character;

using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly IRealmService _realmService;

        public CharactersController(ICharacterService characterService, IRealmService realmService)
        {
            _characterService = characterService;
            _realmService = realmService;
        }

        public async Task<IActionResult> Character(CharacterInputModel model)
        {
            if (ModelState.IsValid)
            {
                var viewModel = await _characterService.FindCharacterAsync(model);

                if (viewModel != null)
                {
                    return View(viewModel);
                }

                return this.Redirect(string.Format(ViewNames.ErrorPage, HttpStatusCode.NotFound));
            }

            return this.Redirect(string.Format(ViewNames.ErrorPage, HttpStatusCode.InternalServerError));
        }

        [HttpGet]
        public async Task<IActionResult> CharacterForm()
        {
            var viewModel = new CharacterInputModel();
            viewModel.Realms = await _realmService.GetRealmsByRegionAsync(Localizations.Regions.EU);

            return this.PartialView(ViewNames.Partial.CharacterForm, viewModel);
        }
    }
}
