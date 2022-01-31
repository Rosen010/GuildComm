using BNetAPI.Core.Interfaces;
using BNetAPI.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuildComm.Core.Extensions
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorizationData(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(nameof(AuthorizationData));
            var authorizationData = section.Get<AuthorizationData>();
            services.AddSingleton<IAuthorizationData>(authorizationData);
        }
    }
}
