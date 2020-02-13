namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Members;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MembersService : IMembersService
    {
        private readonly GuildCommDbContext context;
        private readonly IGuildsService guildsService;

        public MembersService(GuildCommDbContext context, IGuildsService guildsService)
        {
            this.context = context;
            this.guildsService = guildsService;
        }

        public Member CreateMember(Character character, Guild guild, Rank rank)
        {
            var member = new Member
            {
                Character = character,
                Guild = guild,
                Rank = rank,
                MemberSince = DateTime.UtcNow
            };

            return member;
        }

        public async Task<List<MemberViewModel>> GetAllMembersViewModelAsync(string guildId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Member> GetMemberByIdAsync(string id)
        {
            var member = await this.context.Members.SingleOrDefaultAsync(m => m.Id == id);
            return member;
        }

        public async Task<bool> IsMemberAuthorizedAsync(string memberId, string guildId)
        {
            var member = await this.GetMemberByIdAsync(memberId);
            var guild = await this.guildsService.GetGuildByIdAsync(guildId);

            return member.Guild == guild && (member.Rank == Rank.GuildeMaster || member.Rank == Rank.Officer);
        }
    }
}
