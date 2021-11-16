using AutoMapper;
using GuildComm.Common.Constants.ApiConstants;
using GuildComm.Data.Models;
using GuildComm.Services.Models;
using System;

namespace GuildComm.MappingProfiles
{
    public class AccessTokenProfile : Profile
    {
        public AccessTokenProfile()
        {
            this.CreateMap<AccessToken, AccessToken>();

            this.CreateMap<BNetBearerToken, AccessToken>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => TokenNames.BNetAccessToken))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => DateTime.UtcNow.AddSeconds(src.Expiration)));
        }
    }
}
