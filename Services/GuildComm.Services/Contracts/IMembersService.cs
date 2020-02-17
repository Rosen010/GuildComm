namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Members;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMembersService
    {
        Member CreateMember(Character character, Guild guild, Rank rank);

        Task<Member> GetMemberByIdAsync(string id);

        Task<bool> IsMemberAuthorizedAsync(string memberId, string guildId);
    }
}