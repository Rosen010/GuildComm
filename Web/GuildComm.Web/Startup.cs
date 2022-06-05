namespace GuildComm.Web
{
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using GuildComm.Core.Extensions;
    using GuildComm.Common;
    using GuildComm.Data;
    using GuildComm.MappingProfiles;
    using GuildComm.Web.Extensions;

    using BNetAPI.Core;
    using GuildComm.Core.Configurarions;
    using GuildComm.Common.Constants;

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

            var emailConfig = Configuration
                .GetSection(ConfigurationConstants.EmailConfiguration)
                .Get<EmailConfiguration>();

            services.AddSingleton(this.Configuration);
            services.AddSingleton(emailConfig);

            services.ConfigureBNetDependencies();
            services.AddAuthorizationData();
            services.RegisterDependencies();

            services.ConfigureIdentity();

            services.AddMvc();
            services.AddHttpClient();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDbContext<GuildCommDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddDbContext<GuildCommIdentityDbContext>(options => options.UseSqlServer("name=ConnectionStrings:IdentityConnection"));
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

            app.UseStatusCodePagesWithReExecute(ViewNames.ErrorPage);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "profile",
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
