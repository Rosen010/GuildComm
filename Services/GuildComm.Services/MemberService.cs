namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Members;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MemberService : IMemberService
    {
        private readonly GuildCommDbContext context;
        private readonly IGuildsService guildsService;

        public MemberService(GuildCommDbContext context, IGuildsService guildsService)
        {
            this.context = context;
            this.guildsService = guildsService;
        }

        public List<MemberViewModel> GetAllMembersViewModel(string guildId)
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
