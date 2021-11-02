using GuildComm.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GuildComm.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGuildService _guildService;

        public SearchController(IGuildService guildService)
        {
            _guildService = guildService;
        }
    }
}
