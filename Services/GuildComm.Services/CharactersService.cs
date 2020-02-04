namespace GuildComm.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<List<CharacterViewModel>> GetUserCharactersViewModelAsync()
        {
            var user = await usersService.GetUserAsync();

            var characters = await context.Characters
                .Include(c => c.Realm)
                .Where(c => c.UserId == user.Id)
                .Select(c => this.mapper.Map<CharacterViewModel>(c))
                .ToListAsync();

            return characters;
        }

        public async Task<List<Character>> GetUserCharactersAsync(GuildCommUser user)
        {
            var currentUser = await usersService.GetUserAsync();

            var characters = await context.Characters
                .Include(c => c.Guild)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            return characters;
        }

        public async Task<CharacterDetailsViewModel> GetCharacterAsync(int id)
        {
            var character = await context.Characters
                .Include(c => c.Guild)
                .SingleOrDefaultAsync(c => c.Id == id);

            var charModel = this.mapper.Map<CharacterDetailsViewModel>(character);

            return charModel;
        }

        public async Task RemoveCharacterAsync(int id)
        {
            var character = await context.Characters.SingleOrDefaultAsync(c => c.Id == id);

            this.context.Characters.Remove(character);
            await this.context.SaveChangesAsync();
        }
    }
}
