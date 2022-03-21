using GuildComm.Core.Interfaces;
using GuildComm.Core.Factories.Interfaces;
using GuildComm.Web.Models.Home;
using GuildComm.Web.Models.Guild;
using System.Threading.Tasks;

namespace GuildComm.Core.Factories
{
    public class HomePageFactory : IHomePageFactory
    {
        private readonly IRealmService _realmService;

        public HomePageFactory(IRealmService realmService)
        {
            _realmService = realmService;
        }

        public async Task<HomePageViewModel> CreateViewModelAsync(string region)
        {
            var viewModel = new HomePageViewModel();
            viewModel.Form = new GuildInputModel();
            viewModel.Form.Realms = await _realmService.GetRealmsByRegionAsync(region);

            return viewModel;
        }
    }
}
