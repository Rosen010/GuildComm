namespace GuildComm.Services.Data.Utilities
{
    using AutoMapper;
    using System.Linq;

    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Web.ViewModels.Characters;
    using GuildComm.Web.ViewModels.Users;

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

            this.CreateMap<Character, CharacterViewModel>()
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : "N/A"));

            this.CreateMap<Character, CharacterDetailsViewModel>()
                .ForMember(x => x.Class, y => y.MapFrom(s => s.Class.ToString()))
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()))
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : "N/A"));

            //User
            this.CreateMap<GuildCommUser, GuildCommUserDetailsViewModel>()
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : "N/A"));
        }
    }


}
