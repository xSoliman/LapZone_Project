using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly LapZoneContext _db;

        public ProductDetailsController(LapZoneContext db)
        {
            _db = db;
        }

        public IActionResult Index(int id)
        {
            var product = _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return View("ProductNotFound");
            }

            int? userId = HttpContext.Session.GetInt32("UserId");

            bool isInWishlist = false;
            if (userId != null)
            {
                isInWishlist = _db.Wishlists.Any(w => w.UserId == userId && w.ProductId == id);
            }

            ViewBag.Product = product;
            ViewBag.IsInWishlist = isInWishlist;

            ViewBag.Message = TempData["Message"];

            // Pass the product to the view
            return View(product);
        }
    }
}
