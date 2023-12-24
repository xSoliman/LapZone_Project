using LapZone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Controllers;
public class CartController : Controller
{
    private readonly LapZoneContext _db;

    public CartController(LapZoneContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var cart = await _db.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                    .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.UserId == user.UserId);

        if (cart == null)
        {
            cart = new Cart { UserId = user.UserId };
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
        }

        var insufficientStockItems = new List<CartItem>();

        foreach (var cartItem in cart.CartItems)
        {
            if (cartItem.Quantity > cartItem.Product.StockQuantity)
            {
                insufficientStockItems.Add(cartItem);
            }
        }

        if (insufficientStockItems.Any())
        {
            ViewBag.ErrorMessage = "Sorry, there is not enough stock for the following items:";
            ViewBag.insufficientStockItems = insufficientStockItems;
            return View(cart.CartItems);
        }

        return View(cart.CartItems);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            TempData["ErrorMessage"] = "Please login first.";
            return RedirectToAction("Login", "Account");
        }

        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            TempData["ErrorMessage"] = "Please login first.";
            return RedirectToAction("Login", "Account");
        }

        var cart = _db.Carts.Include(c => c.CartItems)
            .FirstOrDefault(c => c.UserId == user.UserId);

        if (cart == null)
        {
            cart = new Cart { UserId = user.UserId };
            _db.Carts.Add(cart);
            _db.SaveChanges();
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        quantity = Math.Max(quantity, 1);

        if (cartItem == null)
        {
            cartItem = new CartItem { ProductId = productId, Quantity = quantity };
            cart.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity += quantity;
        }

        _db.SaveChanges();
        TempData["Message"] = "Item added to cart successfully.";

        return RedirectToAction("Index", "ProductDetails", new { id = productId });
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var cartItem = await _db.CartItems
            .Include(c => c.Cart)
            .Include(c => c.Product)
            .FirstOrDefaultAsync(m => m.CartItemId == id);

        _db.CartItems.Remove(cartItem);
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private bool CartItemExists(int id)
    {
        return (_db.CartItems?.Any(e => e.CartItemId == id)).GetValueOrDefault();
    }

    public IActionResult DecreaseQuantity(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            TempData["ErrorMessage"] = "Please login first.";
            return RedirectToAction("Login", "Account");
        }

        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            TempData["ErrorMessage"] = "Please login first.";
            return RedirectToAction("Login", "Account");
        }

        var cart = _db.Carts.Include(c => c.CartItems)
                            .ThenInclude(ci => ci.Product)
                            .FirstOrDefault(c => c.UserId == user.UserId);

        if (cart == null)
        {
            TempData["ErrorMessage"] = "Your cart is empty.";
            return RedirectToAction("Index", "Cart");
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == id);

        if (cartItem == null)
        {
            TempData["ErrorMessage"] = "Product not found in the cart.";
            return RedirectToAction("Index", "Cart");
        }

        cartItem.Quantity = cartItem.Product.StockQuantity;

        _db.SaveChanges();

        return RedirectToAction("Index", "Cart");
    }
}

