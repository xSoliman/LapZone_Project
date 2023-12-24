using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Controllers
{
    public class WishlistController : Controller
    {
        private readonly LapZoneContext _db;

        public WishlistController(LapZoneContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var wishlistProductIds = _db.Wishlists
                    .Where(w => w.UserId == userId.Value)
                    .Select(w => w.ProductId)
                    .ToList();

                var wishlistProducts = _db.Products
                    .Where(p => wishlistProductIds.Contains(p.ProductId))
                    .ToList();

                return View(wishlistProducts);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult AddToWishlist(int productId, string sourcePage)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                if (!_db.Wishlists.Any(w => w.UserId == userId.Value && w.ProductId == productId))
                {
                    var wishlistItem = new Wishlist { UserId = userId.Value, ProductId = productId };
                    _db.Wishlists.Add(wishlistItem);
                    _db.SaveChanges();
                }

                return RedirectToActionBySourcePage(sourcePage, productId);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult RemoveFromWishlist(int productId, string sourcePage)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var wishlistItem = _db.Wishlists.FirstOrDefault(w => w.UserId == userId.Value && w.ProductId == productId);

                if (wishlistItem != null)
                {
                    _db.Wishlists.Remove(wishlistItem);
                    _db.SaveChanges();
                }

                return RedirectToActionBySourcePage(sourcePage, productId);
            }

            return RedirectToAction("Login", "Account");
        }
        private IActionResult RedirectToActionBySourcePage(string sourcePage, int productId)
        {
            if (sourcePage == "ProductDetails")
            {
                return RedirectToAction("Index", "ProductDetails", new { id = productId });
            }
            else if (sourcePage == "Home")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (sourcePage == "Store")
            {
                return RedirectToAction("Index", "ProductStore");
            }
            else
            {
                return RedirectToAction("Index", "Wishlist");
            }
        }
    }
}
