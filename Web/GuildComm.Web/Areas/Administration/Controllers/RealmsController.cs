namespace GuildComm.Web.Areas.Administration.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.ViewModels.Realms;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [Area("Administration")]
    public class RealmsController : Controller
    {
        private readonly IRealmsService realmsService;

        public RealmsController(IRealmsService realmsService)
        {
            this.realmsService = realmsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealmCreateInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.realmsService.CreateRealmAsync(inputModel);
            }
            else
            {
                return this.View();
            }

            return this.RedirectToAction("Index", "Administration");
        }
    }
}
