using GuildComm.Common.Constants;
using GuildComm.Services.Contracts;
using GuildComm.Services.Models;
using GuildComm.Services.Models.Settings;
using GuildComm.Services.Settings.Contracts;
using GuildComm.Services.Utilities.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuildComm.Services.Clients
{
    public class BNetApiClient : IBNetApiClient
    {
        private HttpClient _authClient;

        private readonly ISettingsManager _settingsManager;

        public BNetApiClient(ISettingsManager settingsReader)
        {
            _authClient = new HttpClient();
            _settingsManager = settingsReader;
        }

        public async Task Authenticate()
        {
            var settings = _settingsManager.LoadSection<BNetApiSettings>();

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

                        this.SaveSettings(response);
                    }
                }
            }
        }

        public async Task ValidateToken()
        {
            var settings = _settingsManager.LoadSection<AccessTokenSettings>();

            if (settings != null)
            {
                var expiration = DateTime.ParseExact(settings.Expires, DateFormats.DateToSeconds, CultureInfo.InvariantCulture);

                if (expiration <= DateTime.UtcNow.AddMinutes(-1))
                {
                    await this.Authenticate();
                }
            }
        }

        private void SaveSettings(BNetBearerToken token)
        {
            var expiration = DateTime.UtcNow.AddSeconds(token.Expiration);

            _settingsManager.UpdateSection("AccessToken", "token", token.AccessToken);
            _settingsManager.UpdateSection("AccessToken", "expires", expiration.ToString());
        }
    }
}
