namespace GuildComm.Web
{
    using GuildComm.Data;
    using GuildComm.Services;
    using GuildComm.Data.Models;
    using GuildComm.Data.Seeding;
    using GuildComm.Web.Extensions;
    using GuildComm.Services.Data.Utilities;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using AutoMapper;

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

            services.AddAutoMapper(typeof(GuildCommProfile));

            services.AddScoped<GuildCommUserRoleSeeder>();
            services.AddScoped<GuildCommRealmSeeder>();

            services.AddTransient<IRealmsService, RealmsService>();
            services.AddTransient<IGuildsService, GuildsService>();

            services.AddSingleton(this.Configuration);
            //services.AddHttpContextAccessor();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDatabaseSeeding();

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

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
