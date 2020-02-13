using System.Threading.Tasks;
using GuildComm.Web.ViewModels.Applications;

namespace GuildComm.Services
{
    public interface IApplicationsService
    {
        Task CreateApplicationAsync(ApplicationCreateInputModel inputModel);
    }
}
