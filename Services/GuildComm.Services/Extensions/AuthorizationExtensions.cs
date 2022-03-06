using BNetAPI.Core.Interfaces;
using BNetAPI.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GuildComm.Core.Extensions
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorizationData(this IServiceCollection services)
        {
            services.AddSingleton((serviceProvider) => GetAuthorizationData(serviceProvider));
        }

        private static IAuthorizationData GetAuthorizationData(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var section = configuration
                .GetSection(nameof(AuthorizationData))
                .Get<AuthorizationData>(c => c.BindNonPublicProperties = true);

            return section;
        }
    }
}
