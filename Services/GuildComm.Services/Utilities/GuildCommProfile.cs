namespace GuildComm.Services.Data.Utilities
{
    using AutoMapper;
    using System.Linq;

    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;

    public class GuildCommProfile : Profile
    {
        public GuildCommProfile()
        {
            this.CreateMap<GuildCreateInputModel, Guild>()
                .ForMember(x => x.Realm, y => y.MapFrom(s => s.Realm));

            this.CreateMap<Guild, GuildsAllViewModel>()
                .ForMember(x => x.MembersCount, y => y.MapFrom(s => s.Members.Count()));
        }
    }


}
