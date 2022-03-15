using AutoMapper;
using BNetAPI.Guilds.Models.RequestModels;
using BNetAPI.Guilds.Models.ResponseModels;
using GuildComm.Common.Constants;
using GuildComm.Common.Extensions;
using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Search;

namespace GuildComm.MappingProfiles
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            this.CreateMap<SearchViewModel, GuildRequestModel>()
                .ForMember(dest => dest.GuildName, opt => opt.MapFrom(src => src.GuildName.Replace(' ', '-').ToLower()))
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.Replace(' ', '-').ToLower()))
                .ForMember(dest => dest.Locale, opt => opt.MapFrom(src => Localizations.MappedLocalizations[src.Namespace]))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => Localizations.MappedRegions[src.Namespace]));

            this.CreateMap<GuildResponse, GuildViewModel>()
                .ForMember(g => g.Realm, opt => opt.MapFrom(src => src.Realm.Slug.CapitalizeFirstLetter()));
        }
    }
}
