namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Applications;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApplicationsService : IApplicationsService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;

        public ApplicationsService(GuildCommDbContext context,
            IUsersService usersService)
        {
            this.context = context;
            this.usersService = usersService;
        }

        public async Task CreateApplicationAsync(ApplicationCreateInputModel inputModel)
        {
            var user = await this.usersService.GetUserAsync();

            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Id == inputModel.GuildId);

            var character = await this.context.Characters
                .SingleOrDefaultAsync(dbChar => dbChar.Name == inputModel.CharacterName);

            var application = new Application
            {
                CharacterName = inputModel.CharacterName,
                Age = inputModel.Age,
                Country = inputModel.Country,
                Role = (Role)(Enum.Parse(typeof(Role), inputModel.Role)),
                Experience = inputModel.Experience,
                ArmoryLink = inputModel.ArmoryLink,
                Character = character,
                Guild = guild
            };

            await this.context.Applications.AddAsync(application);
            await this.context.SaveChangesAsync();
        }

        public async Task<ApplicationDetailsViewModel> GetApplicationByIdAsync(int applicationId)
        {
            var application = await this.context.Applications
                .Include(a => a.Guild)
                .Where(a => a.Id == applicationId)
                .Select(a => new ApplicationDetailsViewModel
                {
                    Id = a.Id,
                    CharacterName = a.CharacterName,
                    Age = a.Age,
                    Country = a.Country,
                    Role = a.Role.ToString(),
                    Experience = a.Experience,
                    ArmoryLink = a.ArmoryLink,
                    GuildId = a.GuildId
                })
                .SingleOrDefaultAsync();

            return application;
        }

        public async Task<List<ApplicationViewModel>> GetAllGuildApplications(string guildId)
        {
            var applications = await this.context.Applications
                .Include(a => a.Guild)
                .Where(a => a.GuildId == guildId)
                .Select(a => new ApplicationViewModel
                {
                    Id = a.Id,
                    CharacterName = a.CharacterName,
                    Country = a.Country,
                    Role = a.Role.ToString(),
                    GuildId = guildId
                })
                .ToListAsync();

            return applications;
        }

        public async Task Dismiss(int applicationId)
        {
            var application = await this.context
                .Applications
                .SingleOrDefaultAsync(a => a.Id == applicationId);

            this.context.Applications.Remove(application);
            await this.context.SaveChangesAsync();
        }
    }
}
