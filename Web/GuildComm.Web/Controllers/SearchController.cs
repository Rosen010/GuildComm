﻿using GuildComm.Services.Contracts;
using GuildComm.Web.Models.Search;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGuildService _guildService;

        public SearchController(IGuildService guildService)
        {
            _guildService = guildService;
        }

        [HttpGet]
        public async Task<IActionResult> Guilds(SearchInputModel model)
        {
            var guild = await _guildService.FindGuiild(model);

            return this.View(guild);
        }
    }
}