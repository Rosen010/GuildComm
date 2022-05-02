using GuildComm.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace GuildComm.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegistrationInputModel userModel)
        {
            return this.View();
        }
    }
}
