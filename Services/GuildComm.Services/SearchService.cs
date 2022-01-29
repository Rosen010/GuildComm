using BNetAPI.Core.Interfaces;
using GuildComm.Core.Interfaces;

namespace GuildComm.Services
{
    public class SearchService : ISearchService
    {
        private readonly IBNetApiClient _apiClient;

        public SearchService(IBNetApiClient apiClient)
        {
            _apiClient = apiClient;
        }
    }
}
