using AutoMapper;
using GuildComm.Data.Models;
using GuildComm.Web.ViewModels;

namespace GuildComm.Services.Data.Utilities
{
    public class GuildCommProfile : Profile
    {
        public GuildCommProfile()
        {
            this.CreateMap<GuildCreateInputModel, Guild>()
                .ForMember(x => x.Realm, y => y.MapFrom(s => s.Realm));
        }
    }


}
