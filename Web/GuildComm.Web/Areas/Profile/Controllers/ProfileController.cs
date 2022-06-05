using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuildComm.Web.Areas.Controllers
{
    [Authorize]
    [Area("profile")]
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return this.View();
        }
    }
}
