namespace GuildComm.Services.Utilities
{
    using AutoMapper;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Users;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<GuildCommUser, GuildCommUserDetailsViewModel>();
        }
    }
}
