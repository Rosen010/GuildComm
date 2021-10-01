namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Users;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> Details()
        {
            GuildCommUserDetailsViewModel userViewModel = await this.usersService.GetUserViewModelAsync();

            return this.View(userViewModel);
        }

        [Authorize]
        public IActionResult UpdateDescription()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateDescription(GuildCommUserDescriptionUpdateInputModel inputModel)
        {
            await this.usersService.UpdateUserDescriptionAsync(inputModel);

            return RedirectToAction("Details", "Users");
        }
    }
}