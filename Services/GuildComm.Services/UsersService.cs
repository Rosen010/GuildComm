namespace GuildComm.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly GuildCommDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly IMapper mapper;

        public UsersService(GuildCommDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;

            this.mapper = mapper;
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

            var user = this.mapper.Map<GuildCommUserDetailsViewModel>(userFromDb);

            return user;
        }

        public async Task UpdateUserDescriptionAsync(GuildCommUserDescriptionUpdateInputModel inputModel)
        {
            var user = await this.GetUserAsync();
            user.Description = inputModel.Description;

            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
