namespace GuildComm.Services.Data.Utilities
{
    using AutoMapper;
    using System.Linq;

    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Web.ViewModels.Characters;

    public class GuildCommProfile : Profile
    {
        public GuildCommProfile()
        {
            // Guild
            this.CreateMap<GuildCreateInputModel, Guild>()
                .ForMember(x => x.Realm, y => y.MapFrom(s => s.Realm));

            this.CreateMap<Guild, GuildsAllViewModel>()
                .ForMember(x => x.MembersCount, y => y.MapFrom(s => s.Members.Count()));

            //Character
            this.CreateMap<CharacterRegisterInputModel, Character>();
        }
    }


}
