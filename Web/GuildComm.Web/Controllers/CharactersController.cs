﻿namespace GuildComm.Web.Controllers
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
            inputModel.Realms = await this.realmsService.GetAllRealmViewModelsAsync();

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CharacterRegisterInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await this.charactersService.CreateCharacterAsync(inputModel);
                return this.RedirectToAction("Details", "Users");
            }

            return this.View(inputModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var character = await this.charactersService.GetCharacterAsync(id);

            return this.View(character);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await this.charactersService.RemoveCharacterAsync(id);

            return this.RedirectToAction("Details", "Users");
        }
    }
}
