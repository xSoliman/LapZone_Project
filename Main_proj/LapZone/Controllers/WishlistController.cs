using Microsoft.AspNetCore.Mvc;

namespace LapZone.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
