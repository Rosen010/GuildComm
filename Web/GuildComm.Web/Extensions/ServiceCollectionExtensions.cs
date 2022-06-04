using GuildComm.Core;
using GuildComm.Core.Factories;
using GuildComm.Core.Factories.Interfaces;
using GuildComm.Core.Interfaces;
using GuildComm.Data.Repositories;
using GuildComm.Data.Repositories.Interfaces;
using GuildComm.Data;
using GuildComm.Services;
using GuildComm.Web.TokenProviders;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using GuildComm.Data.Models.Identity;
using System;

namespace GuildComm.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddTransient<IGuildService, GuildService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IRealmService, RealmService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddTransient<IRealmsRepository, RealmsRepository>();
            services.AddTransient<IHomePageFactory, HomePageFactory>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<GuildCommUser, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                })
                .AddEntityFrameworkStores<GuildCommIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<GuildCommUser>>("emailconfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(3));
        }
    }
}
