using Microsoft.AspNetCore.Mvc;

namespace LapZone.Controllers
{
	public class StoreController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
