using GuildComm.Services.Contracts;

namespace GuildComm.Services
{
    public class SearchService : ISearchService
    {
        private readonly IBNetApiClient _apiClient;

        public SearchService(IBNetApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public void GetGuild()
        {

        }
    }
}
