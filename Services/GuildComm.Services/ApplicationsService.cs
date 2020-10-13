namespace GuildComm.Services
{
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Common.Constants;
    using GuildComm.Web.ViewModels.Applications;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationsService : IApplicationsService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;

        private readonly IMapper mapper;

        public ApplicationsService(GuildCommDbContext context,
            IUsersService usersService,
            IMapper mapper)
        {
            this.context = context;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public async Task CreateApplicationAsync(ApplicationCreateInputModel inputModel)
        {
            var user = await this.usersService.GetUserAsync();

            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Id == inputModel.GuildId);

            if (guild == null)
            {
                throw new InvalidOperationException(ExceptionMessages.GuildNotFound);
            }

            var character = await this.context.Characters
                .SingleOrDefaultAsync(dbChar => dbChar.Name == inputModel.CharacterName);

            if (character == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterNotFound);
            }

            var application = this.mapper.Map<Application>(inputModel);
            application.Guild = guild;
            application.Character = character;

            await this.context.Applications.AddAsync(application);
            await this.context.SaveChangesAsync();
        }

        public async Task<ApplicationDetailsViewModel> GetApplicationByIdAsync(int applicationId)
        {
            var application = await this.context.Applications
                .Include(a => a.Guild)
                .Where(a => a.Id == applicationId)
                .Select(a => mapper.Map<ApplicationDetailsViewModel>(a))
                .SingleOrDefaultAsync();

            if (application == null)
            {
                throw new InvalidOperationException(ExceptionMessages.ApplicationNotFound);
            }

            return application;
        }

        public async Task<List<ApplicationsAllViewModel>> GetAllGuildApplications(string guildId)
        {
            var applications = await this.context.Applications
                .Include(a => a.Guild)
                .Where(a => a.GuildId == guildId)
                .Select(a => mapper.Map<ApplicationsAllViewModel>(a))
                .ToListAsync();

            return applications;
        }

        public async Task Dismiss(int applicationId)
        {
            var application = await this.context
                .Applications
                .SingleOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
            {
                throw new InvalidOperationException(ExceptionMessages.ApplicationNotFound);
            }

            this.context.Applications.Remove(application);
            await this.context.SaveChangesAsync();
        }
    }
}
