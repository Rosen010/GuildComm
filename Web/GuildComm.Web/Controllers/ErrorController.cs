using GuildComm.Common;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace GuildComm.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            if (code == (int)HttpStatusCode.NotFound)
            {
                return this.View(ViewNames.Shared.NotFound);
            }
            
            return this.View(ViewNames.Shared.Error);
        }
    }
}
