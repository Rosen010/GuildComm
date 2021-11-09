using GuildComm.Common.Constants;
using GuildComm.Services.Contracts;
using GuildComm.Services.Contracts.Clients;
using GuildComm.Services.Models;
using GuildComm.Services.Models.Settings;
using GuildComm.Services.Utilities.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GuildComm.Services.Clients
{
    public class BNetApiClient : IBNetApiClient
    {
        private readonly IRestClient _restClient;

        private readonly IConfiguration _configuration;

        public BNetApiClient(IConfiguration configuration, IRestClient restClient)
        {
            _restClient = restClient;
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken()
        {
            var token = new AccessTokenSettings();
            _configuration.GetSection("BNetAccessToken").Bind(token);

            var isValid = await this.ValidateToken(token);

            if (!isValid)
            {
                _configuration.GetSection("BNetAccessToken").Bind(token);
            }

            return token.Token;
        }

        private async Task Authenticate()
        {
            var settings = new BNetApiSettings();
            _configuration.GetSection("BNetApi").Bind(settings);

            if (settings != null)
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoints.BNetOauth))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.ClientId + ":" + settings.ClientSecret));

                    var data = new Dictionary<string, string>
                    {
                        { ApiRequestConstants.Headers.GrantType, ApiRequestConstants.GrantTypes.ClientCredentials },
                    };

                    httpRequest.Headers.Authorization = new AuthenticationHeaderValue(ApiRequestConstants.AuthenticationType.Basic, credentials);
                    httpRequest.Content = new FormUrlEncodedContent(data);

                    var response = await _restClient.SendRequest<BNetBearerToken>(httpRequest);

                    this.SaveSettings(response);
                }
            }
        }

        private async Task<bool> ValidateToken(AccessTokenSettings token)
        {
            if (!string.IsNullOrEmpty(token?.Token))
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
                await this.Authenticate();
                return false;
            }

            return true;
        }

        private void SaveSettings(BNetBearerToken token)
        {
            var expiration = DateTime.UtcNow.AddSeconds(token.Expiration).ToString(DateFormats.DateToSeconds, CultureInfo.InvariantCulture);
            var settings = new AccessTokenSettings { Token = token.AccessToken, Expires = expiration };

            _configuration.GetSection("BNetAccessToken:token").Value = settings.Token;
            _configuration.GetSection("BNetAccessToken:expires").Value = settings.Expires;
        }
    }
}
