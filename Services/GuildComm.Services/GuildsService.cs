﻿namespace GuildComm.Services
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
    using GuildComm.Web.ViewModels.Guild;
    using GuildComm.Web.ViewModels.Characters;

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

            if (inputModel.Realm.Guilds.Any(g => g.Name == guild.Name))
            {
                throw new InvalidOperationException();
            }
            else
            {
                await this.context.Guilds.AddAsync(guild);
                await this.context.SaveChangesAsync();

                await this.AddMemberAsync(inputModel.Character, Rank.GuildeMaster, guild);
            }
        }
           
        public async Task<Guild> GetGuildAsync(string name)
        {
            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Name == name);
            return guild;
        }

        public async Task<GuildDetailsViewModel> GetGuildByIdAsync(string id)
        {
            var guildFromDb = await this.context.Guilds
                .Include(c => c.Realm)
                .SingleOrDefaultAsync(g => g.Id == id);

            var characters = await this.context.Characters
                .Include(c => c.Guild)
                .Where(c => c.GuildId == id)
                .Select(c => this.mapper.Map<CharacterViewModel>(c))
                .ToListAsync();

            var guildModel = new GuildDetailsViewModel
            {
                Name = guildFromDb.Name,
                RealmName = guildFromDb.Realm.Name,
                Characters = characters,
                Realm = guildFromDb.Realm
            };

            return guildModel;
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
            if (character.Realm == guild.Realm && character.Guild == null)
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
            else
            {
                this.context.Guilds.Remove(guild);
                await this.context.SaveChangesAsync();

                throw new InvalidOperationException();
            }      
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
