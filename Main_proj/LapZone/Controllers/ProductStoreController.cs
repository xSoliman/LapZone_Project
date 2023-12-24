using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Linq;

namespace LapZone.Controllers;

public class ProductStoreController : Controller
{
    private readonly LapZoneContext _db;

    public ProductStoreController(LapZoneContext db)
    {
        _db = db;
    }


    //Make the index page keep the choosen filters and sort
    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        dynamic viewModel = new ExpandoObject();

        var products = _db.Products.ToList();
        viewModel.Products = products;

        var categories = _db.Categories.ToList();
        viewModel.Categories = categories;

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

    public IActionResult Search(string searchTerm, int? categoryId)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        dynamic viewModel = new ExpandoObject();

        var query = _db.Products.AsQueryable();

        // Filter by search term
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p => p.ProductName.Contains(searchTerm));
        }

        // Filter by category
        if (categoryId.HasValue && categoryId.Value != 0)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (userId != null)
        {
            var wishlistProductIds = _db.Wishlists.Where(w => w.UserId == userId).Select(w => w.ProductId).ToList();
            viewModel.InWishList = wishlistProductIds;
        }
        else
        {
            viewModel.InWishList = new List<int>(); // User is not logged in, so nothing is in the wishlist
        }

        viewModel.Products = query.ToList();

        viewModel.Categories = _db.Categories.ToList();

        return View("Index", viewModel);
    }


    //Remain: sort by popular, sort opation a-z z-a, keep the choosen filter and sort after click 
    public IActionResult FilterAndSort(string sortBy, List<int> categoriesId, decimal minprice, decimal maxprice, bool? sortAsc = false)
    {
        IQueryable<Product> query = _db.Products;

        // Apply sorting
        switch (sortBy)
        {
            case "1": // Sort by Price
                query = sortAsc.HasValue && sortAsc.Value ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                break;
            case "2": // Sort by Name
                query = sortAsc.HasValue && sortAsc.Value ? query.OrderBy(p => p.ProductName) : query.OrderByDescending(p => p.ProductName);
                break;
            case "3": // Sort by Popular
                query = sortAsc.HasValue && sortAsc.Value ? query.OrderBy(p => p.OrderItems.Count) : query.OrderByDescending(p => p.OrderItems.Count);
                break;
            default: // Default: Sort by Popular (you may modify this based on your business logic)
                query = sortAsc.HasValue && sortAsc.Value ? query.OrderBy(p => p.OrderItems.Count) : query.OrderByDescending(p => p.OrderItems.Count);
                break;
        }

        // Apply category filter
        if (categoriesId != null && categoriesId.Count > 0)
        {
            query = query.Where(p => categoriesId.Contains(p.CategoryId ?? 0));
        }

        // Apply price filter
        if (minprice > 0)
        {
            query = query.Where(p => p.Price >= minprice);
        }

        if (maxprice > 0)
        {
            query = query.Where(p => p.Price <= maxprice);
        } 
        var filteredAndSortedProducts = query.ToList();
        dynamic viewModel = new ExpandoObject();
        viewModel.Products = filteredAndSortedProducts;
        viewModel.Categories = _db.Categories.ToList();

        // Continue with the rest of your logic to prepare the view model
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId != null)
        {
            var wishlistProductIds = _db.Wishlists.Where(w => w.UserId == userId).Select(w => w.ProductId).ToList();
            viewModel.InWishList = wishlistProductIds;
        }
        else
        {
            viewModel.InWishList = new List<int>(); // User is not logged in, so nothing is in the wishlist
        }

        return View("Index", viewModel);
    }


}
