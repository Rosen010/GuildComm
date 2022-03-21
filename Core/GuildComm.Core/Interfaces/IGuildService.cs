using GuildComm.Web.Models.Guild;
using System.Threading.Tasks;

namespace GuildComm.Core.Interfaces
{
    public interface IGuildService
    {
        Task<GuildViewModel> FindGuiildAsync(GuildInputModel model);
    }
}
