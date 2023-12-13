using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace LapZone.Controllers
{
    public class HomeController : Controller
    {
      /*  private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/
        private readonly LapZoneContext _db;
        public HomeController(LapZoneContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           var sess = HttpContext.Session.GetString("Email");

            dynamic viewModel = new ExpandoObject();

            var products = _db.Products.ToList();
            viewModel.Products = products;

            var categories = _db.Categories.ToList();
            viewModel.Categories = categories;

            return View(viewModel);

        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
