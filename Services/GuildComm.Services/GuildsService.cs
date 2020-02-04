namespace GuildComm.Services
{
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class GuildsService : IGuildsService
    {
        private readonly GuildCommDbContext context;
        private readonly IRealmsService realmsService;
        private readonly ICharactersService charactersService;
        private readonly IUsersService usersService;

        private readonly IMapper mapper;

        public GuildsService(GuildCommDbContext context, 
            IRealmsService realmsService, 
            ICharactersService charactersService, 
            IUsersService usersService,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

            this.realmsService = realmsService;
            this.charactersService = charactersService;
            this.usersService = usersService;
        }

        public async Task CreateGuildAsync(GuildCreateInputModel inputModel)
        {
            var user = await this.usersService.GetUserAsync();
            var character = await this.charactersService.GetUserCharactersAsync(user);

            inputModel.Realm = await realmsService.GetRealmByNameAsync(inputModel.RealmName);
            inputModel.Character = character.SingleOrDefault(c => c.Name == inputModel.MasterCharacter);

            Guild guild = this.mapper.Map<Guild>(inputModel);

            await this.context.Guilds.AddAsync(guild);
            await this.context.SaveChangesAsync();

            await this.AddMemberAsync(inputModel.Character, Rank.GuildeMaster, guild);    
        }

        public async Task<Guild> GetGuildAsync(string name)
        {
            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Name == name);
            return guild;
        }

        public async Task<List<GuildsAllViewModel>> GetAllGuildsAsync()
        {
            List<GuildsAllViewModel> guilds = new List<GuildsAllViewModel>();

            var guildsFromDb = await this.context.Guilds
                .Include(g => g.Realm)
                .Include(g => g.Members)
                .ToListAsync();

            foreach (var guild in guildsFromDb)
            {
                var guildToAdd = this.mapper.Map<GuildsAllViewModel>(guild);
                guilds.Add(guildToAdd);
            }

            return guilds;
        }

        public async Task AddMemberAsync(Character character, Rank rank, Guild guild)
        {
            var member = new Member
            {
                Character = character,
                Guild = guild,
                Rank = rank,
                MemberSince = DateTime.UtcNow
            };

            character.Guild = guild;
            guild.Members.Add(member);

            this.context.Members.Add(member);
            this.context.Guilds.Update(guild);
            this.context.Characters.Update(character);

            await this.context.SaveChangesAsync();
        }

        public async Task<List<GuildsAllViewModel>> GetUserGuildsAsync()
        {
            var user = await this.usersService.GetUserAsync();
            var characters = await this.charactersService.GetUserCharactersAsync(user);

            var guilds = new List<GuildsAllViewModel>();

            foreach (var character in characters)
            {
                if (character.Guild != null) 
                {
                    var guildFromDb = await this.context.Guilds
                        .Include(g => g.Members)
                        .SingleOrDefaultAsync(g => g.Id == character.Guild.Id);

                    var guildToAdd = this.mapper.Map<GuildsAllViewModel>(guildFromDb);

                    guilds.Add(guildToAdd);
                }      
            }

            return guilds;
        }
    }
}
