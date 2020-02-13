namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Applications;
    using System;
    using System.Threading.Tasks;

    public class ApplicationsService : IApplicationsService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;
        private readonly IGuildsService guildsService;

        public ApplicationsService(GuildCommDbContext context, IUsersService usersService, IGuildsService guildsService)
        {
            this.context = context;
            this.usersService = usersService;
            this.guildsService = guildsService;
        }

        public async Task CreateApplicationAsync(ApplicationCreateInputModel inputModel)
        {
            var application = new Application
            {
                CharacterName = inputModel.CharacterName,
                Age = inputModel.Age,
                Country = inputModel.Country,
                Role = (Role)(Enum.Parse(typeof(Role), inputModel.Role)),
                Experience = inputModel.Experience,
                ArmoryLink = inputModel.ArmoryLink,
                User = await this.usersService.GetUserAsync(),
                Guild = await this.guildsService.GetGuildByIdAsync(inputModel.GuildId)
            };

            await this.context.Applications.AddAsync(application);
            await this.context.SaveChangesAsync();
        }
    }
}
