using GuildComm.Common.Constants;
using GuildComm.Data.Models;
using GuildComm.Data.Repositories.Contracts;
using GuildComm.Services.Contracts;
using GuildComm.Services.Contracts.Clients;
using GuildComm.Services.Models;
using GuildComm.Services.Models.Settings;
using GuildComm.Services.Utilities.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        private readonly ITokenRepository _tokenRepository;

        public BNetApiClient(IConfiguration configuration, IRestClient restClient, ITokenRepository tokenRepository)
        {
            _restClient = restClient;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var token = await _tokenRepository.GetTokenAsync(TokenNames.BNetAccessToken);

            token = await this.ValidateTokenAsync(token);
            return token.Value;
        }

        private async Task<AccessToken> AuthenticateAsync()
        {
            var settings = new BNetApiSettings();
            _configuration.GetSection("BNetApi").Bind(settings);

            AccessToken token = null;

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

                    var response = await _restClient.SendRequestAsync<BNetBearerToken>(httpRequest);

                    token = new AccessToken
                    {
                        Name = TokenNames.BNetAccessToken,
                        Value = response.AccessToken,
                        Expiration = DateTime.UtcNow.AddSeconds(response.Expiration)
                    };

                    await _tokenRepository.UpdateTokenAsync(token);
                }
            }

            return token;
        }

        private async Task<AccessToken> ValidateTokenAsync(AccessToken token)
        {
            if (token != null)
            {
                if (token.Expiration <= DateTime.UtcNow.AddMinutes(-10))
                {
                    token = await this.AuthenticateAsync();
                }
            }
            else
            {
                token = await this.AuthenticateAsync();
            }

            return token;
        }
    }
}
