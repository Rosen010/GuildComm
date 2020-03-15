namespace GuildComm.Web
{
    using GuildComm.Data;
    using GuildComm.Services;
    using GuildComm.Data.Models;
    using GuildComm.Data.Seeding;
    using GuildComm.Web.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using GuildComm.Services.Utilities;
    using System.Linq;
    using GuildComm.Services.Contracts;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GuildCommDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                m => m.MigrationsAssembly("GuildComm.Data")));
        
            services.AddIdentity<GuildCommUser, IdentityRole>()
                .AddEntityFrameworkStores<GuildCommDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Login";
            });

            services.AddAutoMapper(AutoMapperConfig.GetAutoMapperProfilesFromAllAssemblies()
            .ToArray());

            services.AddScoped<GuildCommUserCharacterSeeder>();
            services.AddScoped<GuildCommUserSeeder>();
            services.AddScoped<GuildCommUserRoleSeeder>();
            services.AddScoped<GuildCommRealmSeeder>();
            
            
            services.AddTransient<IMembersService, MembersService>();
            services.AddTransient<IRealmsService, RealmsService>();
            services.AddTransient<IGuildsService, GuildsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICharactersService, CharactersService>();
            services.AddTransient<IApplicationsService, ApplicationsService>();
            services.AddTransient<IEventsService, EventsService>();
            
            services.AddSingleton(this.Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDatabaseSeeding();
            app.UseCharacterSeeding();

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<GuildCommDbContext>())
                
            context.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
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
