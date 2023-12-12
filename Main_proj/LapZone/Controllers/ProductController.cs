using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
namespace LapZone.Controllers;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;


public class ProductController : Controller
{
    private readonly LapZoneContext _db;
    public ProductController(LapZoneContext db)
    {
        _db = db;

    }
    public IActionResult Index()
    {
        List<Product> objProductList = _db.Products.ToList();

        return View(objProductList);
    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Create(Product obj, IEnumerable<IFormFile> Images)
    {
        
            _db.Products.Add(obj);

            _db.SaveChanges();
            return RedirectToAction("Index");


    }
}
