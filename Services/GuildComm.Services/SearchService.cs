using GuildComm.Services.Contracts;
using GuildComm.Services.Models.ResponseModels;

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
