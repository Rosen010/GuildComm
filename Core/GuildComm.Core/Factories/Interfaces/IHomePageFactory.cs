using GuildComm.Web.Models.Home;
using System.Threading.Tasks;

namespace GuildComm.Core.Factories.Interfaces
{
    public interface IHomePageFactory
    {
        Task<HomePageViewModel> CreateViewModelAsync(string region);
    }
}
