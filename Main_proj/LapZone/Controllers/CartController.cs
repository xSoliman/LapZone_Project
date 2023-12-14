using Microsoft.AspNetCore.Mvc;

namespace LapZone.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
