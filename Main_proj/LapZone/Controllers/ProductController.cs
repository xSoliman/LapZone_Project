using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
namespace LapZone.Controllers;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


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
		List<Product> objProductList = _db.Products.ToList();

		return View(objProductList);
	}
	public IActionResult Create()
	{

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Product obj)
	{

		/*if (ModelState.IsValid)
		{*/
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
			TempData["successData"] = "Item has been added successfully";
			return RedirectToAction("Index");

/*
		}
		else
		{
			return View(obj);
		}*/

	}
}
