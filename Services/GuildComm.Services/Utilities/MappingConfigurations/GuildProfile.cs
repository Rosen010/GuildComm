using AutoMapper;
using GuildComm.Services.Models.RequestModels;
using GuildComm.Services.Models.ResponseModels;
using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Search;

namespace GuildComm.Core.Utilities.MappingConfigurations
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            this.CreateMap<SearchInputModel, GuildRequestModel>()
                .ForMember(dest => dest.GuildName, opt => opt.MapFrom(src => src.GuildName.ToLower()))
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.ToLower()));

            this.CreateMap<GuildResponse, GuildViewModel>()
                .ForMember(g => g.Realm, opt => opt.MapFrom(src => src.Realm.Slug));
        }
    }
}
