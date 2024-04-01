using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapZone.Models;
using LapZone.ViewModels;

namespace LapZone.Controllers
{
    public class ProductController : Controller
    {
        private readonly LapZoneContext _context;
        private readonly IHostingEnvironment _host;

        public ProductController(LapZoneContext db, IHostingEnvironment host)
        {
            _context = db;
            _host = host;

        }
        // GET: Product
        public async Task<IActionResult> Index()
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            var lapZoneContext = _context.Products.Include(p => p.Category);
            return View(await lapZoneContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                // Validate image file type
                if (product.clientFile != null)
                {
                    if (!product.clientFile.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("clientFile", "Invalid file type. Please upload an image.");
                        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                    // Validate image file size (adjust the limit as needed)
                    const int maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB
                    if (product.clientFile.Length > maxFileSizeInBytes)
                    {
                        ModelState.AddModelError("clientFile", "File size exceeds the allowed limit (5 MB).");
                        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                    // Save the image file if provided
                    string upload = Path.Combine(_host.WebRootPath, "Images/Products");

                    string uniqueFileName = $"{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}-{product.clientFile.FileName}";
                    string fullPath = Path.Combine(upload, uniqueFileName);

                    product.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    product.ImagePath = uniqueFileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }


        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Price,CategoryId,StockQuantity,ImagePath")] Product product)
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int? userRole = HttpContext.Session.GetInt32("RoleId");

            if (userRole == null || userRole != 1)
                return RedirectToAction("Index", "Home");

            if (_context.Products == null)
            {
                return Problem("Entity set 'LapZoneContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
