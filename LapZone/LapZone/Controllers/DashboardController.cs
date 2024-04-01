using Microsoft.AspNetCore.Mvc;

namespace LapZone.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            return View(); // Return without a model
        }
    }
}

