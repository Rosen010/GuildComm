using BNetAPI.Core.Utilities.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Character;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public async Task<IActionResult> Character(string realm, string characterName)
        {
            var model = new CharacterInputModel() 
            {
                Realm = realm.ToLower(),
                CharacterName = characterName.ToLower(),
                NameSpace = Parameters.Namespace.ProfileEU,
                Locale = Parameters.Locale.GB,
            };

            var viewModel = await _characterService.FindCharacter(model);
            return this.View(viewModel);
        }
    }
}
