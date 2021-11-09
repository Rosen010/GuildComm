using GuildComm.Services.Contracts;
using GuildComm.Services.Contracts.Clients;
using GuildComm.Services.Enums;
using GuildComm.Services.Models.RequestModels;
using GuildComm.Services.Models.ResponseModels;
using GuildComm.Services.Utilities.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace GuildComm.Services.Clients
{
    public class BNetGuildClient : IBNetGuildClient
    {
        private readonly IBNetApiClient _apiClient;

        private readonly IRestClient _restClient;

        public BNetGuildClient(IBNetApiClient apiClient, IRestClient restClient)
        {
            _apiClient = apiClient;
            _restClient = restClient;
        }

        public async Task<GuildResponse> RetrieveGuild(GuildRequestModel request)
        {
            var token = await _apiClient.GetAccessToken();
            var endpoint = string.Format(Endpoints.Guild, request.Realm, request.GuildName);

            var builder = new UriBuilder(endpoint);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query[BNetRequestHeaders.Namespace.ToString()] = request.NameSpace;
            query[BNetRequestHeaders.Locale.ToString()] = Parameters.Locale.GB;

            builder.Query = query.ToString();
            var url = builder.ToString();

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url))
            {
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue(ApiRequestConstants.AuthenticationType.Bearer, token);

                var response = await _restClient.SendRequest<GuildResponse>(httpRequest);
                return response;
            };
        }
    }
}
