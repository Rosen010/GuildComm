namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ICharactersService charactersService;

        public UsersController(IUsersService usersService, ICharactersService charactersService)
        {
            this.usersService = usersService;
            this.charactersService = charactersService;
        }

        public async Task<IActionResult> Details()
        {
            GuildCommUserDetailsViewModel userViewModel = await this.usersService.GetUserViewModelAsync();

            userViewModel.Characters = await this.charactersService.GetUserCharactersAsync();

            return this.View(userViewModel);
        }
    }
}
