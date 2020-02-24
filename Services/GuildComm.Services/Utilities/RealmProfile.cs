namespace GuildComm.Services.Utilities
{
    using AutoMapper;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Realms;

    public class RealmProfile : Profile
    {
        public RealmProfile()
        {
            this.CreateMap<Realm, RealmViewModel>()
                .ForMember(x => x.Type, y => y.MapFrom(s => s.RealmType.ToString()));
        }
    }
}
