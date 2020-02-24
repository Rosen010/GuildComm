namespace GuildComm.Services.Utilities
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Web.ViewModels.Guild;

    using AutoMapper;
    using System.Linq;

    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            this.CreateMap<Guild, GuildsAllViewModel>()
               .ForMember(x => x.MembersCount, y => y.MapFrom(s => s.Members.Count()));

            this.CreateMap<Guild, GuildDetailsViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.RealmName, y => y.MapFrom(s => s.Realm.Name))
                .ForMember(x => x.RealmRegion, y => y.MapFrom(s => s.Realm.Region.ToString()));

            this.CreateMap<Guild, GuildManageViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.RealmName, y => y.MapFrom(s => s.Realm.Name))
                .ForMember(x => x.RealmRegion, y => y.MapFrom(s => s.Realm.Region.ToString()));
        }
    }
}
