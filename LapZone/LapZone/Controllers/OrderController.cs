using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LapZone.Controllers
{
    public class OrderController : Controller
    {
        private readonly LapZoneContext _db;

        public OrderController(LapZoneContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sourcePage)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please login first.";
                return RedirectToAction("Login", "Account");
            }

            var user = _db.Users
                .Include(u => u.Addresses)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Please login first.";
                return RedirectToAction("Login", "Account");
            }

            var cart = _db.Carts.Include(c => c.CartItems)
                                .ThenInclude(ci => ci.Product)
                                    .ThenInclude(p => p.Category)
                             .FirstOrDefault(c => c.UserId == user.UserId);

            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.Quantity > cartItem.Product.StockQuantity)
                {
                    return RedirectToAction("Index", "Cart");
                }
            }

            var order = new Order
            {
                UserId = user.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItems.Sum(item => item.Product.Price * item.Quantity),
                OrderItems = cart.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList()
            };

            ViewBag.Order = order;
            ViewBag.CartItems = cart.CartItems;
            ViewBag.UserAddresses = user.Addresses;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(int addressId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please login first.";
                return RedirectToAction("Login", "Account");
            }

            var user = _db.Users
                .Include(u => u.Addresses)
                .FirstOrDefault(u => u.UserId == userId);

            var cart = _db.Carts.Include(c => c.CartItems)
                                .ThenInclude(ci => ci.Product)
                                    .ThenInclude(p => p.Category)
                             .FirstOrDefault(c => c.UserId == user.UserId);

            if (cart == null || cart.CartItems.Count == 0)
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserId = user.UserId,
                OrderDate = DateTime.Now,
                AddressId = addressId,
                TotalAmount = cart.CartItems.Sum(item => item.Product.Price * item.Quantity),
                OrderItems = cart.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList()
            };
            order.TotalAmount += 50;

            _db.Orders.Add(order);
            _db.SaveChanges();

            foreach (var orderItem in order.OrderItems)
            {
                var product = _db.Products.Find(orderItem.ProductId);

                if (product != null)
                {
                    product.StockQuantity -= orderItem.Quantity;
                }
            }

            _db.SaveChanges();

            _db.CartItems.RemoveRange(cart.CartItems);
            _db.SaveChanges();

            return RedirectToAction("ThankYou", new { orderId = order.OrderId });
        }

        public IActionResult ThankYou(int orderId)
        {
            var order = _db.Orders.Include(o => o.Address).FirstOrDefault(o => o.OrderId == orderId);
            ViewBag.Order = order;

            return View();
        }
    }
}
