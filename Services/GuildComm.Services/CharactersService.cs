namespace GuildComm.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Common.Constants;
    using GuildComm.Web.ViewModels.Characters;

    public class CharactersService : ICharactersService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;

        private readonly IMapper mapper;

        public CharactersService(GuildCommDbContext context, IUsersService usersService, IMapper mapper)
        {
            this.context = context;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public async Task CreateCharacterAsync(CharacterRegisterInputModel inputModel)
        {
            var character = this.mapper.Map<Character>(inputModel);

            var realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Name == inputModel.RealmName);

            if (realm == null)
            {
                throw new InvalidOperationException(ExceptionMessages.RealmNotFound);
            }

            character.Realm = realm;
            character.User = await usersService.GetUserAsync();

            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<T>> GetUserCharactersAsync<T>()
        {
            var user = await usersService.GetUserAsync();

            var characters = await context.Characters
                .Include(c => c.Realm)
                .Include(c => c.Guild)
                .Where(c => c.UserId == user.Id)
                .Select(c => this.mapper.Map<T>(c))
                .ToListAsync();

            return characters;
        }

        public async Task<List<T>> GetCharactersByNameAsync<T>(string name)
        {
            var characters = await context.Characters
                .Include(c => c.Guild)
                .Include(c => c.Realm)
                .Where(c => c.Name == name)
                .Select(c => this.mapper.Map<T>(c))
                .ToListAsync();

            return characters;
        }

        public async Task<T> GetCharacterAsync<T>(int id)
        {
            var character = await context.Characters
                .Include(c => c.Guild)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterNotFound);
            }

            var charModel = this.mapper.Map<T>(character);

            return charModel;
        }

        public async Task RemoveCharacterAsync(int id)
        {
            var character = await context.Characters.SingleOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterNotFound);
            }

            this.context.Characters.Remove(character);
            await this.context.SaveChangesAsync();
        }
    }
}
