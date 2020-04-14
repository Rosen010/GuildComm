namespace GuildComm.Web.Areas.Administration.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Users;

    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [Area("Administration")]
    public class UserAdministrationController : Controller
    {
        private readonly IUsersService usersService;

        private const int ItemsPerPage = 5;

        public UserAdministrationController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new GuildCommUsersListingModel();

            model.Users = await usersService.GetAllUsersAsync(ItemsPerPage, (page - 1) * ItemsPerPage);
            var count = this.usersService.GetUsersCount();

            model.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = page;

            return this.View(model);
        }

        public async Task<IActionResult> Ban(string id)
        {
            await this.usersService.RemoveUserAsync(id);

            return this.RedirectToAction("Index", "UserAdministration");
        }
    }
}
