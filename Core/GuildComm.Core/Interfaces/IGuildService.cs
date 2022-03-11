using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Search;
using System.Threading.Tasks;

namespace GuildComm.Core.Interfaces
{
    public interface IGuildService
    {
        Task<GuildViewModel> FindGuiildAsync(SearchInputModel model);
    }
}
