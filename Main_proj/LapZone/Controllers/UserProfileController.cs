using Microsoft.AspNetCore.Mvc;

namespace LapZone.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
