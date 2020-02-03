namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IGuildsService guildsService;
        private readonly ICharactersService charactersService;

        public UsersController(IUsersService usersService, IGuildsService guildsService, ICharactersService charactersService)
        {
            this.usersService = usersService;
            this.guildsService = guildsService;
            this.charactersService = charactersService;
        }

        public async Task<IActionResult> Details()
        {
            GuildCommUserDetailsViewModel userViewModel = await this.usersService.GetUserViewModelAsync();

            userViewModel.Characters = await this.charactersService.GetUserCharactersViewModelAsync();
            userViewModel.Guilds = await this.guildsService.GetUserGuildsAsync();

            return this.View(userViewModel);
        }

        public IActionResult UpdateDescription()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDescription(GuildCommUserDescriptionUpdateInputModel inputModel)
        {
            await this.usersService.UpdateUserDescriptionAsync(inputModel);

            return RedirectToAction("Details", "Users");
        }
    }
}
