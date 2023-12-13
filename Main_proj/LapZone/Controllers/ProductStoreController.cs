using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace LapZone.Controllers
{
	public class ProductStoreController : Controller
	{
		private readonly LapZoneContext _db;
		public ProductStoreController(LapZoneContext db)
		{
			_db = db;
		}
		public IActionResult Index(Product prod)
		{
            var sess = HttpContext.Session.GetString("Email");

            dynamic viewModel = new ExpandoObject();

			var products = _db.Products.ToList(); 
			viewModel.Products = products;

			var categories = _db.Categories.ToList();
			viewModel.Categories = categories;

			return View(viewModel);


		}
	}
}
