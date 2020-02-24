namespace GuildComm.Services
{
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Members;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class GuildsService : IGuildsService
    {
        private readonly GuildCommDbContext context;
        private readonly IUsersService usersService;

        private readonly IMapper mapper;

        public GuildsService(GuildCommDbContext context,
            IUsersService usersService,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

            this.usersService = usersService;
        }

        public async Task CreateGuildAsync(GuildCreateInputModel inputModel)
        {
            var user = await this.usersService.GetUserAsync();

            var characters = await context.Characters
                .Include(c => c.Guild)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            var realm = await this.context.Realms
                .SingleOrDefaultAsync(dbRealm => dbRealm.Name == inputModel.RealmName);

            var character = characters.SingleOrDefault(c => c.Name == inputModel.MasterCharacter);

            var guild = new Guild
            {
                Name = inputModel.Name,
                Realm = realm,
                GuildMaster = inputModel.MasterCharacter
            };

            if (realm.Guilds.Any(g => g.Name == guild.Name))
            {
                throw new InvalidOperationException();
            }
            else
            {
                await this.context.Guilds.AddAsync(guild);
                await this.context.SaveChangesAsync();

                await this.AddMemberAsync(character.Id, "GuildeMaster", guild.Id);
            }
        }
           
        public async Task<Guild> GetGuildByIdAsync(string id)
        {
            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Id == id);
            return guild;
        }

        public async Task<T> GetGuildViewModelByIdAsync<T>(string id)
        {
            var guildFromDb = await this.context.Guilds
                .Include(c => c.Realm)
                .SingleOrDefaultAsync(g => g.Id == id);

            var members = await this.context.Members
                .Include(c => c.Guild)
                .Include(c => c.Character)
                .Where(c => c.GuildId == id)
                .Select(c => this.mapper.Map<MemberViewModel>(c))
                .ToListAsync();

            var guildModel = this.mapper.Map<T>(guildFromDb);

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

        public async Task AddMemberAsync(int characterId, string rank, string guildId)
        {
            var character = await this.context.Characters
                .Include(c => c.Realm)
                .SingleOrDefaultAsync(c => c.Id == characterId);

            var guild = await this.context.Guilds.SingleOrDefaultAsync(g => g.Id == guildId);

            if (character.Realm == guild.Realm && character.Guild == null)
            {
                var member = new Member
                {
                    Character = character,
                    Guild = guild,
                    Rank = (Rank)(Enum.Parse(typeof(Rank), rank)),
                    MemberSince = DateTime.UtcNow
                };

                character.Guild = guild;
                character.MemberId = member.Id;
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
            var characters = await context.Characters
                .Include(c => c.Guild)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

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

        public async Task<bool> IsUserInTargetGuild(string guildId)
        {
            var user = await this.usersService.GetUserAsync();

            return this.context.Characters
                .Include(c => c.Guild)
                .Include(c => c.User)
                .Where(c => c.User.UserName == user.UserName)
                .Any(c => c.GuildId == guildId);
        }

        public async Task RemoveMemberAsync(string id)
        {
            var member = await this.context.Members
                .SingleOrDefaultAsync(m => m.Id == id);

            this.context.Members.Remove(member);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveGuildAsync(string id)
        {
            var guild = await GetGuildByIdAsync(id);

            var characters = this.context.Characters
                .Include(c => c.Guild)
                .Where(c => c.GuildId == id)
                .ToList();

            var members = this.context.Members
                .Include(m => m.Guild)
                .Where(m => m.GuildId == id)
                .ToList();

            foreach (var character in characters)
            {
                character.Guild = null;
                this.context.Characters.Update(character);
            }

            foreach (var member in members)
            {
                this.context.Members.Remove(member);
            }

            context.Guilds.Remove(guild);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsUserAuthorized(string guildId)
        {
            var user = await this.usersService.GetUserAsync();
            var guild = await this.context.Guilds
                .Where(g => g.Id == guildId)
                .SingleOrDefaultAsync();

            return user.Characters.Any(c => c.Name == guild.GuildMaster);            
        }
    }
}