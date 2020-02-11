namespace GuildComm.Services
{
    using GuildComm.Web.ViewModels.Members;
    using System.Collections.Generic;

    public interface IMemberService
    {
        List<MemberViewModel> GetAllMembersViewModel(string guildId);
    }
}
