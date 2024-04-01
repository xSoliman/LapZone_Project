using LapZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AddressesController : Controller
{
    private readonly LapZoneContext _context;

    public AddressesController(LapZoneContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var userAddresses = await _context.Addresses
            .Include(a => a.User)
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return View(userAddresses);
    }

    // GET: Addresses/Create
    public IActionResult Create()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", userId);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AddressId,Country,Governorate,City,AddressLine")] Address address)
    {
        if (!ModelState.IsValid)
            return View(address);

        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        address.UserId = userId.Value;

        _context.Add(address);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Addresses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        if (id == null || _context.Addresses == null)
        {
            return NotFound();
        }

        // Explicitly include the User navigation property
        var address = await _context.Addresses
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.AddressId == id && a.UserId == userId);

        if (address == null)
        {
            return NotFound();
        }

        // Ensure the address belongs to the logged-in user
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

        if (user == null || address.UserId != user.UserId)
        {
            // Handle the case where the user does not exist or the address doesn't belong to the user.
            // You might want to redirect to a login page or show an error message.
            return RedirectToAction("Login", "Account");
        }

        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", address.UserId);
        return View(address);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AddressId,UserId,Country,Governorate,City,AddressLine")] Address address)
    {
        if (!ModelState.IsValid)
            return View(address);

        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }
    
        // Ensure the user exists
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            // Handle the case where the user does not exist.
            // You might want to redirect to a login page or show an error message.
            return RedirectToAction("Login", "Account");
        }

        if (id != address.AddressId)
        {
            return NotFound();
        }

        // Check if the address belongs to the logged-in user
        var existingAddress = await _context.Addresses.FindAsync(id);

        if (existingAddress == null || existingAddress.UserId != user.UserId)
        {
            // Handle the case where the address doesn't belong to the logged-in user.
            // You might want to redirect or show an error message.
            return NotFound();
        }

        var ordersWithCurrentAddress = await _context.Orders
            .Where(o => o.AddressId == existingAddress.AddressId && o.UserId == user.UserId)
            .ToListAsync();

        if (ordersWithCurrentAddress.Any())
        {
            // Create a new address for the user with the updated information
            var newAddress = new Address
            {
                UserId = user.UserId,
                Country = address.Country,
                Governorate = address.Governorate,
                City = address.City,
                AddressLine = address.AddressLine
            };

            _context.Add(newAddress);
            await _context.SaveChangesAsync();

            existingAddress.UserId = null;
            _context.Update(existingAddress);
            await _context.SaveChangesAsync();
        }
        else
        {
            // Update the properties of the existing address without modifying the UserId
            existingAddress.Country = address.Country;
            existingAddress.Governorate = address.Governorate;
            existingAddress.City = address.City;
            existingAddress.AddressLine = address.AddressLine;

            _context.Update(existingAddress);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Log or handle concurrency exception
            return RedirectToAction(nameof(Index));
        }
        TempData["SuccessMessage"] = "Address updated successfully.";

        return View();
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

        var address = await _context.Addresses.FindAsync(id);

        // Ensure the address belongs to the logged-in user
        if (address == null || address.UserId != userId)
        {
            // Handle the case where the address doesn't belong to the logged-in user.
            // You might want to redirect or show an error message.
            return RedirectToAction("Login", "Account");
        }

        var ordersWithCurrentAddress = await _context.Orders
            .Where(o => o.AddressId == address.AddressId && o.UserId == userId)
            .ToListAsync();

        if (ordersWithCurrentAddress.Any())
        {
            // There are orders associated with the address, make UserId null
            address.UserId = null;
            _context.Update(address);
        }
        else
        {
            // No orders associated with the address, delete the address
            _context.Addresses.Remove(address);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AddressExists(int id)
    {
        return (_context.Addresses?.Any(e => e.AddressId == id)).GetValueOrDefault();
    }
}
