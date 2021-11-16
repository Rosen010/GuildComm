﻿using System.Net.Http;
using System.Threading.Tasks;

namespace GuildComm.Services.Contracts.Clients
{
    public interface IRestClient
    {
        Task<TResponse> SendRequestAsync<TResponse>(HttpRequestMessage requestMessage);
    }
}
