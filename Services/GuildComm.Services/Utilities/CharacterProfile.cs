namespace GuildComm.Services.Utilities
{
    using AutoMapper;
    using GuildComm.Common;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;

    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            this.CreateMap<CharacterRegisterInputModel, Character>();

            this.CreateMap<Character, CharacterViewModel>()
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : GlobalConstants.NoValueTemplate))
                .ForMember(x => x.RealmName, y => y.MapFrom(s => s.Realm.Name))
                .ForMember(x => x.RealmRegion, y => y.MapFrom(s => s.Realm.Region.ToString()));

            this.CreateMap<Character, CharacterDetailsViewModel>()
                .ForMember(x => x.Class, y => y.MapFrom(s => s.Class.ToString()))
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()))
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : GlobalConstants.NoValueTemplate))
                .ForMember(x => x.GuildRegion, y => y.MapFrom(s => s.Guild != null ? s.Guild.Realm.Region.ToString() : GlobalConstants.NoValueTemplate));
        }
    }
}
