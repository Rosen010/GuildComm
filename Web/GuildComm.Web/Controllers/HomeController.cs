using System.Diagnostics;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GuildComm.Services;
using GuildComm.Web.ViewModels;

namespace GuildComm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGuildsService guildsService;

        public HomeController(ILogger<HomeController> logger, IGuildsService guildsService)
        {
            _logger = logger;
            this.guildsService = guildsService;
        }

        public async Task<IActionResult> Index()
        {
            var popularGuilds = await guildsService.GetPopularGuildsAsync();

            return View(popularGuilds);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
