using AutoMapper;
using BNetAPI.Guilds.Models.RequestModels;
using BNetAPI.Guilds.Models.ResponseModels;
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
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.Replace(' ', '-').ToLower()))
                .ForMember(dest => dest.NameSpace, opt => opt.MapFrom(src => src.Namespace.Split()[0]))
                .ForMember(dest => dest.Locale, opt => opt.MapFrom(src => src.Namespace.Split()[1]));

            this.CreateMap<GuildResponse, GuildViewModel>()
                .ForMember(g => g.Realm, opt => opt.MapFrom(src => src.Realm.Slug));
        }
    }
}
