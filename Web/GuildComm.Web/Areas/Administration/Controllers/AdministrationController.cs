namespace GuildComm.Web.Areas.Administration.Controllers
{
    using GuildComm.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    [Area(GlobalConstants.AdministrationArea)]
    public class AdministrationController : Controller
    {

    }
}
