using GuildComm.Common.Constants;
using GuildComm.Services.Contracts;
using GuildComm.Services.Contracts.Clients;
using GuildComm.Services.Models;
using GuildComm.Services.Models.Settings;
using GuildComm.Services.Settings.Contracts;
using GuildComm.Services.Utilities.Constants;
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
        private readonly IRestClient _restClient;

        private readonly ISettingsManager _settingsManager;

        public BNetApiClient(ISettingsManager settingsReader, IRestClient restClient)
        {
            _restClient = restClient;
            _settingsManager = settingsReader;
        }

        public async Task<string> GetAccessToken()
        {
            var token = _settingsManager.LoadSection<AccessTokenSettings>();

            var isValid = await this.ValidateToken(token);

            if (!isValid)
            {
                token = _settingsManager.LoadSection<AccessTokenSettings>();
            }

            return token.Token;
        }

        private async Task Authenticate()
        {
            var settings = _settingsManager.LoadSection<BNetApiSettings>();

            if (settings != null)
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoints.BNetOauth))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.ClientId + ":" + settings.ClientSecret));

                    var data = new Dictionary<string, string>
                    {
                        { ApiRequestConstants.Headers.GrantType, ApiRequestConstants.GrantTypes.ClientCredentials },
                    };

                    httpRequest.Headers.Add(ApiRequestConstants.Headers.Authorization, string.Format(ApiRequestConstants.HeaderValues.BearerTokenFormat, credentials));
                    httpRequest.Content = new FormUrlEncodedContent(data);

                    var response = await _restClient.Post<BNetBearerToken>(httpRequest);

                    this.SaveSettings(response);
                }
            }
        }

        private async Task<bool> ValidateToken(AccessTokenSettings token)
        {
            if (token != null)
            {
                var expiration = DateTime.ParseExact(token.Expires, DateFormats.DateToSeconds, CultureInfo.InvariantCulture);

                if (expiration <= DateTime.UtcNow.AddMinutes(-1))
                {
                    await this.Authenticate();
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private void SaveSettings(BNetBearerToken token)
        {
            var expiration = DateTime.UtcNow.AddSeconds(token.Expiration).ToString(DateFormats.DateToSeconds, CultureInfo.InvariantCulture);
            var settings = new AccessTokenSettings { Token = token.AccessToken, Expires = expiration };

            _settingsManager.UpdateSection<AccessTokenSettings>(settings);
        }
    }
}
