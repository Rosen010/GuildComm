namespace GuildComm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ApplicationsController : Controller
    {
        public IActionResult Apply()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            return this.View();
        }
    }
}
