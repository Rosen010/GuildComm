using System.Collections.Generic;
using System.Threading.Tasks;
using GuildComm.Web.ViewModels.Applications;

namespace GuildComm.Services
{
    public interface IApplicationsService
    {
        Task CreateApplicationAsync(ApplicationCreateInputModel inputModel);

        Task Dismiss(int applicationId);

        Task<ApplicationDetailsViewModel> GetApplicationByIdAsync(int applicationId);

        Task<List<ApplicationViewModel>> GetAllGuildApplications(string guildId);
    }
}
