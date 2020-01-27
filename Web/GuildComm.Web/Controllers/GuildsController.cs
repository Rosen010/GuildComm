namespace GuildComm.Web.Controllers
{
    using GuildComm.Services;
    using GuildComm.Web.Models.Guild;
    using Microsoft.AspNetCore.Mvc;

    public class GuildsController : Controller
    {
        private readonly IRealmsService realmsService;

        public GuildsController(IRealmsService realmsService)
        {
            this.realmsService = realmsService;
        }

        public IActionResult Create()
        {
            this.ViewData["Realms"] = this.realmsService.GetAllRealms();

            return this.View();
        }

        //[HttpPost]
        //public IActionResult Create(CreateGuildBindingModel bindingModel)
        //{
        //    if (this.ModelState.IsValid)
        //    {

        //    }
        //}
    }
}
