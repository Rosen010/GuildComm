using AutoMapper;
using BNetAPI.Guilds.Models.RequestModels;
using BNetAPI.Guilds.Models.ResponseModels;
using GuildComm.Common.Extensions;
using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Search;

namespace GuildComm.MappingProfiles
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            this.CreateMap<SearchInputModel, GuildRequestModel>()
                .ForMember(dest => dest.GuildName, opt => opt.MapFrom(src => src.GuildName.Replace(' ', '-').ToLower()))
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.Replace(' ', '-').ToLower()));

            this.CreateMap<GuildResponse, GuildViewModel>()
                .ForMember(g => g.Realm, opt => opt.MapFrom(src => src.Realm.Slug.CapitalizeFirstLetter()));
        }
    }
}
