namespace GuildComm.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using GuildComm.Data;
    using GuildComm.Web.ViewModels.Users;
    using GuildComm.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly GuildCommDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersService(GuildCommDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<GuildCommUser> GetUserAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<GuildCommUserDetailsViewModel> GetUserViewModelAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userFromDb = await context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);

            var user = new GuildCommUserDetailsViewModel
            {
                Username = userFromDb.UserName,
                IsInGuild = userFromDb.IsInGuild,
                Description = userFromDb.Description
            };

            return user;
        }
    }
}
