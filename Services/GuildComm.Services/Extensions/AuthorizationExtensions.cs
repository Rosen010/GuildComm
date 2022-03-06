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
            var section = configuration
                .GetSection(nameof(AuthorizationData))
                .Get<AuthorizationData>(c => c.BindNonPublicProperties = true);

            services.AddSingleton<IAuthorizationData>(section);
        }
    }
}
