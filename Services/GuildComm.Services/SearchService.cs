using GuildComm.Services.Contracts;
using GuildComm.Services.Settings.Contracts;

namespace GuildComm.Services
{
    public class SearchService : ISearchService
    {
        private readonly IBNetApiClient _apiClient;
        private readonly ISettingsManager _settingsManager;

        public SearchService(IBNetApiClient apiClient, ISettingsManager settingsManager)
        {
            _apiClient = apiClient;
            _settingsManager = settingsManager;
        }

        public void GetGuild()
        {

        }
    }
}
