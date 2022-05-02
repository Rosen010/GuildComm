using AutoMapper;

using GuildComm.Data.Models.Identity;
using GuildComm.Web.Models.Account;

namespace GuildComm.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<UserRegistrationInputModel, GuildCommUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
