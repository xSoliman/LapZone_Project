using Microsoft.AspNetCore.Mvc;

namespace LapZone.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
