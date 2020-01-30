namespace GuildComm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CharactersController : Controller
    {
        public IActionResult Register()
        {
            return this.View();
        }
    }
}
