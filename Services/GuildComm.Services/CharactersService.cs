namespace GuildComm.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;

    public class CharactersService : ICharactersService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;
        private readonly IRealmsService realmsService;

        private readonly IMapper mapper;

        public CharactersService(GuildCommDbContext context, IUsersService usersService, IRealmsService realmsService, 
            IMapper mapper)
        {
            this.context = context;
            this.usersService = usersService;
            this.realmsService = realmsService;
            this.mapper = mapper;
        }

        public async Task CreateCharacterAsync(CharacterRegisterInputModel inputModel)
        {
            inputModel.Realm = await realmsService.GetRealmByNameAsync(inputModel.RealmName);
            inputModel.User = await usersService.GetUserAsync();

            var character = this.mapper.Map<Character>(inputModel);

            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();
        }
    }
}
