namespace GuildComm.Web.ViewModels.Members
{
    using GuildComm.Web.ViewModels.Characters;
    using System;

    public class MemberViewModel
    {
        public CharacterViewModel Character { get; set; }

        public string Rank { get; set; }

        public DateTime MemberSince { get; set; }
    }
}
