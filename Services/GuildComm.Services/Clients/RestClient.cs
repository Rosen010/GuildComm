using GuildComm.Services.Contracts.Clients;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GuildComm.Services.Clients
{
    public class RestClient : IRestClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public RestClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TResponse> SendRequestAsync<TResponse>(HttpRequestMessage requestMessage)
        {
            var client = _clientFactory.CreateClient();

            using (var httpResponse = await client.SendAsync(requestMessage))
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(json);

                return response;
            }
        }
    }
}
