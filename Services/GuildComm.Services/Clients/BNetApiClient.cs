using GuildComm.Services.Contracts;
using GuildComm.Services.Models;
using GuildComm.Services.Models.Settings;
using GuildComm.Services.Settings.Contracts;
using GuildComm.Services.Utilities.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuildComm.Services.Clients
{
    public class BNetApiClient : IBNetApiClient
    {
        private BNetBearerToken _accessToken;

        private HttpClient _authClient;

        private readonly ISettingsReader _settingsReader;

        public BNetApiClient(ISettingsReader settingsReader)
        {
            _authClient = new HttpClient();
            _settingsReader = settingsReader;
        }

        public async Task Authenticate()
        {
            var settings = _settingsReader.LoadSection<BNetApiSettings>();

            if (settings != null)
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoints.BNetOauth))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.ClientId + ":" + settings.ClientSecret));

                    var data = new Dictionary<string, string>
                    {
                        { RequestHeaders.GrantType, "client_credentials" },
                    };

                    httpRequest.Headers.Add(RequestHeaders.Authorization, $"Basic {credentials}");
                    httpRequest.Content = new FormUrlEncodedContent(data);

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
}
