using Microsoft.AspNetCore.Mvc;

namespace LapZone.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
