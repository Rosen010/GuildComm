namespace GuildComm.Services
{
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Common.Constants;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Guild;
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

            var realm = await this.context.Realms
                .Include(r => r.Guilds)
                .SingleOrDefaultAsync(dbRealm => dbRealm.Name == inputModel.RealmName);

            var character = await context.Characters
                .Include(c => c.Guild)
                .Include(c => c.Realm)
                .SingleOrDefaultAsync(c => c.Name == inputModel.MasterCharacter && c.Realm.Name == inputModel.RealmName);

            var guild = new Guild
            {
                Name = inputModel.Name,
                Realm = realm,
                GuildMaster = inputModel.MasterCharacter,
                Information = inputModel.Information
            };

            if (realm.Guilds.Any(g => g.Name == guild.Name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.GuildAlreadyExistOnRealm, realm.Name));
            }
            else
            {
                await this.context.Guilds.AddAsync(guild);
                await this.context.SaveChangesAsync();

                await this.AddMemberAsync(character.Id, GuildRanks.GuildMaster, guild.Id);
            }
        }

        public async Task<Guild> GetGuildByIdAsync(string id)
        {
            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Id == id);

            if (guild == null)
            {
                throw new InvalidOperationException(ExceptionMessages.GuildNotFound);
            }

            return guild;
        }

        public async Task<GuildDetailsViewModel> GetGuildViewModelByIdAsync(string id)
        {
            var user = await this.usersService.GetUserAsync();
            var userId = user.Id;

            var guildFromDb = await this.context.Guilds
                .Include(c => c.Realm)
                .SingleOrDefaultAsync(g => g.Id == id);

            var members = await this.context.Members
                .Include(c => c.Guild)
                .Include(c => c.Character)
                .Where(c => c.GuildId == id)
                .Select(c => this.mapper.Map<MemberViewModel>(c))
                .ToListAsync();

            var userCharacters = new List<MemberViewModel>();

            foreach (var member in members)
            {
                if (user.Characters.Any(c => c.MemberId == member.Id))
                {
                    userCharacters.Add(member);
                }
            }

            var guildModel = this.mapper.Map<GuildDetailsViewModel>(guildFromDb);

            if (userCharacters.Any())
            {
                guildModel.UserCharacters = userCharacters;
            }

            return guildModel;
        }

        public async Task<GuildManageViewModel> GetGuildManageViewModelByIdAsync(string id)
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

            var guildModel = this.mapper.Map<GuildManageViewModel>(guildFromDb);

            return guildModel;
        }

        public async Task<List<GuildsAllViewModel>> GetAllGuildsAsync(int? take = null, int skip = 0)
        {
            var guildsFromDb = new List<GuildsAllViewModel>();

            if (take.HasValue)
            {
                guildsFromDb = await this.context.Guilds
                .Include(g => g.Realm)
                .Include(g => g.Members)
                .OrderByDescending(x => x.Members.Count())
                .Skip(skip)
                .Take(take.Value)
                .Select(g => this.mapper.Map<GuildsAllViewModel>(g))
                .ToListAsync();
            }
            else
            {
                guildsFromDb = await this.context.Guilds
                .Include(g => g.Realm)
                .Include(g => g.Members)
                .OrderByDescending(x => x.Members.Count())
                .Skip(skip)
                .Select(g => this.mapper.Map<GuildsAllViewModel>(g))
                .ToListAsync();
            }

            return guildsFromDb;
        }

        public int GetGuildsCount()
        {
            return this.context.Guilds.Count();
        }

        public async Task<List<GuildsAllViewModel>> GetPopularGuildsAsync()
        {
            var guildsFromDb = await this.context.Guilds
               .Include(g => g.Realm)
               .Include(g => g.Members)
               .OrderByDescending(x => x.Members.Count)
               .Take(5)
               .Select(g => this.mapper.Map<GuildsAllViewModel>(g))
               .ToListAsync();

            return guildsFromDb;
        }

        public async Task AddMemberAsync(int characterId, string rank, string guildId)
        {
            var character = await this.context.Characters
                .Include(c => c.Realm)
                .SingleOrDefaultAsync(c => c.Id == characterId);

            if (character == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterNotFound);
            }

            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(g => g.Id == guildId);

            if (guild == null)
            {
                throw new InvalidOperationException(ExceptionMessages.GuildNotFound);
            }

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
                throw new InvalidOperationException(ExceptionMessages.CharacterNotViable);
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
                .Where(c => c.UserId == user.Id)
                .Any(c => c.GuildId == guildId);
        }

        public async Task RemoveMemberAsync(string id)
        {
            var member = await this.context.Members
                .SingleOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                throw new InvalidOperationException(ExceptionMessages.MemberNotFound);
            }

            var character = await this.context.Characters
                .Include(c => c.Guild)
                .Include(c => c.Member)
                .SingleOrDefaultAsync(c => c.MemberId == member.Id);

            character.Guild = null;
            character.MemberId = null;
            this.context.Characters.Update(character);
            this.context.Members.Remove(member);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveGuildAsync(string id)
        {
            var guild = await GetGuildByIdAsync(id);

            if (guild == null)
            {
                throw new InvalidOperationException(ExceptionMessages.GuildNotFound);
            }

            var characters = await this.context.Characters
                .Include(c => c.Guild)
                .Include(c => c.Member)
                .Where(c => c.GuildId == id)
                .ToListAsync();

            var members = await this.context.Members
                .Include(m => m.Guild)
                .Where(m => m.GuildId == id)
                .ToListAsync();

            foreach (var character in characters)
            {
                character.Guild = null;
                character.MemberId = null;
                this.context.Characters.Update(character);
            }

            foreach (var member in members)
            {
                this.context.Members.Remove(member);
            }

            context.Guilds.Remove(guild);
            await context.SaveChangesAsync();
        }

        public async Task PromoteMemberAsync(string id)
        {
            var member = await this.context
                .Members
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member.Rank == Rank.GuildMaster || member.Rank == Rank.Officer)
            {
                throw new InvalidOperationException(ExceptionMessages.MaxMemberRank);
            }

            member.Rank += 1;

            context.Members.Update(member);
            await context.SaveChangesAsync();
        }

        public async Task DemoteMemberAsync(string id)
        {
            var member = await this.context
                .Members
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member.Rank == Rank.Trial)
            {
                throw new InvalidOperationException(ExceptionMessages.MinMemberRank);
            }

            member.Rank -= 1;

            context.Members.Update(member);
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