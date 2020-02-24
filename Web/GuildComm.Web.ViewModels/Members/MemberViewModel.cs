namespace GuildComm.Web.ViewModels.Members
{
    using GuildComm.Web.ViewModels.Characters;
    using System;

    public class MemberViewModel
    {
        public string Id { get; set; }

        public CharacterViewModel Character { get; set; }

        public string Rank { get; set; }

        public string MemberSince { get; set; }
    }
}
