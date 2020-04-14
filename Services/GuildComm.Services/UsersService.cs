namespace GuildComm.Services
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;

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

        public UsersService(GuildCommDbContext context)
        {
            this.context = context;
        }

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
                .Include(u => u.Characters)
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<List<GuildCommUserViewModel>> GetAllUsersAsync(int? take = null, int skip = 0)
        {
            var users = new List<GuildCommUserViewModel>();

            if (take.HasValue)
            {
                users = await this.context.Users
                .Skip(skip)
                .Take(take.Value)
                .Select(u => new GuildCommUserViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
            }
            else
            {
                users = await this.context.Users
                .Skip(skip)
                .Select(u => new GuildCommUserViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
            }

            return users;
        }

        public async Task<GuildCommUserDetailsViewModel> GetUserViewModelAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userFromDb = await context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);

            var user = this.mapper.Map<GuildCommUserDetailsViewModel>(userFromDb);

            return user;
        }

        public int GetUsersCount()
        {
            return this.context.Users.Count();
        }

        public async Task RemoveUserAsync(string id)
        {
            var user = await this.context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new InvalidOperationException("No users with given Id was found");
            }

            this.context.Users.Remove(user);
            await this.context.SaveChangesAsync();
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
