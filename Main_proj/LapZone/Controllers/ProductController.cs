using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace LapZone.Controllers
{
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
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Home");

            List<Product> objProductList = _db.Products.ToList();

            return View(objProductList);
        }

        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Home");

            ViewBag.Categories = new SelectList(_db.Categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Home");

            string fileName = string.Empty;
            if (obj.clientFile != null)
            {
                string upload = Path.Combine(_host.WebRootPath, "Images/Products");

                // Generate a unique file name using a timestamp
                string uniqueFileName = $"{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}-{obj.clientFile.FileName}";
                string fullPath = Path.Combine(upload, uniqueFileName);

                obj.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                obj.ImagePath = uniqueFileName;
            }

            _db.Products.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
