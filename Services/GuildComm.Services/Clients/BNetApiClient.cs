using GuildComm.Services.Models;
using GuildComm.Services.Utilities.Constants;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuildComm.Services.Clients
{
    public class BNetApiClient
    {
        private readonly string _clientId;

        private readonly string _clientSecret;

        private BNetBearerToken _accessToken;

        private static HttpClient _authClient;

        public BNetApiClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task Authenticate()
        {
            var url = "https://us.battle.net/oauth/token";

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(_clientId + ":" + _clientSecret));

                httpRequest.Headers.Add(RequestHeaders.Authorization, $"Basic {credentials}");
                httpRequest.Headers.Add(RequestHeaders.GrantType, "client_credentials");

                using (var httpResponse = await _authClient.SendAsync(httpRequest))
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<BNetBearerToken>(json);

                    _accessToken = response;
                }
            }
        }
    }
}
