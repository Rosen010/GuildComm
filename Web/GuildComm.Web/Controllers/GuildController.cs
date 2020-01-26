namespace GuildComm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GuildController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
        }
    }
}
