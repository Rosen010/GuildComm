﻿using GuildComm.Core.Interfaces;
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

        public async Task<IActionResult> Character(CharacterInputModel model)
        {
            var viewModel = await _characterService.FindCharacterAsync(model);
            return this.View(viewModel);
        }
    }
}
