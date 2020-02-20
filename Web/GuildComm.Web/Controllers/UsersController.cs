namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Users;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using GuildComm.Web.ViewModels.Characters;

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
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            GuildCommUserDetailsViewModel userViewModel = await this.usersService.GetUserViewModelAsync();

            userViewModel.Characters = await this.charactersService.GetUserCharactersViewModelAsync<CharacterViewModel>();
            userViewModel.Guilds = await this.guildsService.GetUserGuildsAsync();

            return this.View(userViewModel);
        }

        public IActionResult UpdateDescription()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDescription(GuildCommUserDescriptionUpdateInputModel inputModel)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            await this.usersService.UpdateUserDescriptionAsync(inputModel);

            return RedirectToAction("Details", "Users");
        }
    }
}
