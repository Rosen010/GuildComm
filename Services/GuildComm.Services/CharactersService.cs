namespace GuildComm.Services
{
    using System;
    using System.Threading.Tasks;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;

    public class CharactersService : ICharactersService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;
        private readonly IRealmsService realmsService;

        public CharactersService(GuildCommDbContext context, IUsersService usersService, IRealmsService realmsService)
        {
            this.context = context;
            this.usersService = usersService;
            this.realmsService = realmsService;
        }

        public async Task CreateCharacterAsync(CharacterRegisterInputModel inputModel)
        {
            inputModel.Realm = await realmsService.GetRealmByNameAsync(inputModel.RealmName);
            inputModel.User = await usersService.GetUserAsync();

            Character character = new Character
            {
                Name = inputModel.Name,
                Role = inputModel.Role,
                Class = inputModel.Class,
                Level = inputModel.Level,
                ItemLevel = inputModel.ItemLevel,
                Realm = inputModel.Realm,
                User = inputModel.User
            };

            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();
        }
    }
}
