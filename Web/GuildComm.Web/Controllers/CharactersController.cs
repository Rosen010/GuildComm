namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Characters;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class CharactersController : Controller
    {
        private readonly IRealmsService realmsService;
        private readonly ICharactersService charactersService;

        public CharactersController(IRealmsService realmsService, ICharactersService charactersService)
        {
            this.realmsService = realmsService;
            this.charactersService = charactersService;
        }

        public async Task<IActionResult> Register()
        {
            CharacterRegisterInputModel inputModel = new CharacterRegisterInputModel();
            inputModel.Realms = await this.realmsService.GetAllRealmsAsync();

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CharacterRegisterInputModel inputModel)
        {
            await this.charactersService.CreateCharacterAsync(inputModel);

            return this.RedirectToAction("All", "Guilds");
        }
    }
}
