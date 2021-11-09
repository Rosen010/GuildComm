using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Search;
using System.Threading.Tasks;

namespace GuildComm.Services.Contracts
{
    public interface IGuildService
    {
        Task<GuildViewModel> FindGuiild(SearchInputModel model);
    }
}
