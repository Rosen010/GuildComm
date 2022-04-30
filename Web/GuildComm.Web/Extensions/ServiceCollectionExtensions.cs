using GuildComm.Core;
using GuildComm.Core.Factories;
using GuildComm.Core.Factories.Interfaces;
using GuildComm.Core.Interfaces;
using GuildComm.Data.Repositories;
using GuildComm.Data.Repositories.Interfaces;
using GuildComm.Data;
using GuildComm.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GuildComm.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddTransient<IGuildService, GuildService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IRealmService, RealmService>();

            services.AddTransient<IRealmsRepository, RealmsRepository>();
            services.AddTransient<IHomePageFactory, HomePageFactory>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<GuildCommIdentityDbContext>();
        }
    }
}
