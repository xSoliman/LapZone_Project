using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace LapZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly LapZoneContext _db;
        public HomeController(LapZoneContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            dynamic viewModel = new ExpandoObject();

            var products = _db.Products.ToList();
            viewModel.Products = products;

            var categories = _db.Categories.ToList();
            viewModel.Categories = categories;

            // Check if the user is logged in
            if (userId != null)
            {
                var wishlistProductIds = _db.Wishlists.Where(w => w.UserId == userId).Select(w => w.ProductId).ToList();

                viewModel.InWishList = wishlistProductIds;
            }
            else
            {
                viewModel.InWishList = new List<int>(); // User is not logged in, so nothing is in the wishlist
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
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
