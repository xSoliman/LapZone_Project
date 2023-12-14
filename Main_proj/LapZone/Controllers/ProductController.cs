using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
namespace LapZone.Controllers;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductController : Controller
{
    private readonly LapZoneContext _db;
    private readonly IHostingEnvironment _host;

    public ProductController(LapZoneContext db, IHostingEnvironment host)
    {
        _db = db;
        _host = host;

    }
    public IActionResult Index()
    {

        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        List<Product> objProductList = _db.Products.ToList();

        return View(objProductList);
    }
    public IActionResult Create()
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");
        ViewBag.Categories = new SelectList(_db.Categories, "CategoryId", "CategoryName");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product obj)
    {
     


        string fileName = string.Empty;
        if (obj.clientFile != null)
        {
            string upload = Path.Combine(_host.WebRootPath, "Images/Products");
            fileName = obj.clientFile.FileName;
            string fullPath = Path.Combine(upload, fileName);
            obj.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
            obj.ImagePath = fileName;
        }

        _db.Products.Add(obj);
        _db.SaveChanges();
        
        return RedirectToAction("Index");


    }
}
