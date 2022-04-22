namespace GuildComm.Web
{
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    using GuildComm.Core;
    using GuildComm.Core.Extensions;
    using GuildComm.Core.Interfaces;
    using GuildComm.Data.Repositories.Interfaces;
    using GuildComm.Data.Repositories;
    using GuildComm.Core.Factories.Interfaces;
    using GuildComm.Core.Factories;
    using GuildComm.Data;
    using GuildComm.MappingProfiles;
    using GuildComm.Services;

    using BNetAPI.Core;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddAutoMapper(AutoMapperConfig.GetAutoMapperProfilesFromAllAssemblies()
            .ToArray());

            services.AddSingleton(this.Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.ConfigureBNetDependencies();
            services.AddAuthorizationData();

            services.AddTransient<IGuildService, GuildService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IRealmService, RealmService>();

            services.AddTransient<IRealmsRepository, RealmsRepository>();
            services.AddTransient<IHomePageFactory, HomePageFactory>();

            services.AddMvc();
            services.AddHttpClient();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDbContext<GuildCommDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/HandleError/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{Controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });       
        }
    }
}
