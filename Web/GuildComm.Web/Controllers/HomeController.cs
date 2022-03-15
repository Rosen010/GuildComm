using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GuildComm.Web.ViewModels;
using GuildComm.Common.Constants;

using System.Threading.Tasks;
using GuildComm.Core.Factories.Interfaces;

namespace GuildComm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageFactory _homePageFactory;

        public HomeController(ILogger<HomeController> logger, IHomePageFactory homePageFactory)
        {
            _logger = logger;
            _homePageFactory = homePageFactory;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _homePageFactory.CreateViewModelAsync(Localizations.Regions.EU);
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
