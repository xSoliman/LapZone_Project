using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace LapZone.Controllers
{
	public class ProductDetailsController : Controller
	{

		private readonly LapZoneContext _db;
		public ProductDetailsController(LapZoneContext db)
		{
			_db = db;
		}
		public IActionResult Index(int id)
		{
			// Retrieve the product based on the provided id
			var product = _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);

			if (product == null)
			{
				// Handle the case where the product with the given id is not found
				return NotFound();
			}

			// Pass the product to the view
			return View(product);
		}
	}
}
