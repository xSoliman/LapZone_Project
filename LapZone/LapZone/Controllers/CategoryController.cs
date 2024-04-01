using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Controllers;
public class CategoryController : Controller
{
    private readonly LapZoneContext _db;
    private readonly IWebHostEnvironment _host;

    public CategoryController(LapZoneContext db, IWebHostEnvironment host)
    {
        _db = db;
        _host = host;
    }

    public IActionResult Index()
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        var ObjCatigoryList = _db.Categories.ToList();

        return View(ObjCatigoryList);
    }

    public IActionResult Create()
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        string fileName = string.Empty;
        if (obj.clientFile != null)
        {
            string upload = Path.Combine(_host.WebRootPath, "Images/Categories");
            fileName = obj.clientFile.FileName;
            string fullPath = Path.Combine(upload, fileName);
            obj.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
            obj.ImagePath = fileName;
        }

        _db.Categories.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "Category created successfully";

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category categoryfromDb = _db.Categories.Find(id);

        if (categoryfromDb == null)
        {
            return NotFound();
        }

        return View(categoryfromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        if (ModelState.IsValid)
        {
            Category oldCategory = _db.Categories.AsNoTracking().FirstOrDefault(c => c.CategoryId == obj.CategoryId);

            string fileName = string.Empty;
            if (obj.clientFile != null)
            {
                string upload = Path.Combine(_host.WebRootPath, "Images/Categories");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}-{obj.clientFile.FileName}";
                string fullPath = Path.Combine(upload, uniqueFileName);
                obj.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                obj.ImagePath = uniqueFileName;
            }

            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }

        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category categoryfromDb = _db.Categories.Find(id);

        if (categoryfromDb == null)
        {
            return NotFound();
        }

        return View(categoryfromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        int? userRole = HttpContext.Session.GetInt32("RoleId");

        if (userRole == null || userRole != 1)
            return RedirectToAction("Index", "Home");

        if (id == null)
        {
            return NotFound();
        }

        Category obj = _db.Categories.Find(id);

        if (obj == null)
        {
            return NotFound();
        }

        var associatedProducts = _db.Products.Where(p => p.CategoryId == obj.CategoryId).ToList();

        if (associatedProducts.Count > 0)
        {
            TempData["error"] = "Cannot delete the category. There are associated products.";
            return RedirectToAction("Delete");
        }

        if (!string.IsNullOrEmpty(obj.ImagePath))
        {
            var imagePath = Path.Combine(_host.WebRootPath, "Images/Categories", obj.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        obj.ImagePath = null;

        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");
    }
}
