using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LapZone.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Linq;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Http;
using System.Text;
using LapZone.ViewModels;


public class UserProfileController : Controller
{
    private readonly LapZoneContext _db;
    private readonly IHostingEnvironment _host;

    public UserProfileController(LapZoneContext db, IHostingEnvironment host)
    {
        _db = db;
        _host = host;
    }

    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        User user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(user);
    }

    public IActionResult OrdersHistory()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            TempData["ErrorMessage"] = "Please login first.";
            return RedirectToAction("Login", "Account");
        }

        var user = _db.Users
            .Include(u => u.Addresses)
            .Include(u => u.Orders)
                .ThenInclude(o => o.Address)
            .Where(u => u.UserId == userId)
            .FirstOrDefault();

        if (user == null)
        {
            TempData["ErrorMessage"] = "User not found.";
            return RedirectToAction("Index", "Home");
        }

        var orders = user.Orders.OrderByDescending(o => o.OrderDate).ToList();

        return View(orders);
    }

    public IActionResult OrderDetails(int orderId)
    {
        var order = _db.Orders
            .Include(o => o.Address)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    public IActionResult UpdateProfile()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        User user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(user);
    }
    [HttpPost]
    public IActionResult UpdateProfile(User updatedUser)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Retrieve the user from the database
        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Update user properties
        user.FullName = updatedUser.FullName;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;

        // Validate image file type
        if (updatedUser.clientFile != null)
        {
            if (!updatedUser.clientFile.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("clientFile", "Invalid file type. Please upload an image.");
                return View(user);
            }

            // Validate image file size (adjust the limit as needed)
            const int maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB
            if (updatedUser.clientFile.Length > maxFileSizeInBytes)
            {
                ModelState.AddModelError("clientFile", "File size exceeds the allowed limit (5 MB).");
                return View(user);
            }

            // Check if a new photo is uploaded
            if (user.ImagePath != null)
            {
                var oldPhoto = Path.Combine(_host.WebRootPath, "Images/Users", user.ImagePath);

                if (System.IO.File.Exists(oldPhoto))
                {
                    System.IO.File.Delete(oldPhoto);
                }
            }

            // Handle file upload
            string uploadPath = Path.Combine(_host.WebRootPath, "Images/Users");

            // Generate a unique file name using a timestamp
            string uniqueFileName = $"{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}-{updatedUser.clientFile.FileName}";
            string fullPath = Path.Combine(uploadPath, uniqueFileName);

            // Copy the uploaded file to the server
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                updatedUser.clientFile.CopyTo(fileStream);
            }

            // Update the user's ImagePath only if a new photo is uploaded
            user.ImagePath = uniqueFileName;
        }

        _db.SaveChanges();

        TempData["SuccessMessage"] = "Profile updated successfully.";
        return View(user); // Return the original user data without clientFile if no new photo is uploaded
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        // Retrieve user ID from the session
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if the user is not authenticated
        }

        // You can load user data and pass it to the view if needed
        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if the user is not found
        }

        return View();
    }

    [HttpPost]
    public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Check if the current password matches the hashed password in the database
        var hashedCurrentPassword = HashPassword(currentPassword);

        if (hashedCurrentPassword != user.PasswordHash)
        {
            ViewBag.ErrorMessage = "Current password is incorrect";
            return View();
        }
     

        // Check if the new password and confirm password match
        if (newPassword != confirmPassword)
        {
            ViewBag.ErrorMessage = "Passwords do not match";
            return View();
        }

        // Check if the new password meets the minimum length requirement
        if (newPassword.Length < 8)
        {
            ViewData["ErrorMessage"] = "Password must be at least 8 characters long";
            return View();
        }

        // Update the user's password in the database
        user.PasswordHash = HashPassword(newPassword);
        _db.SaveChanges();

        TempData["SuccessMessage"] = "Password changed successfully!";

        // Display the success message on the same page
        ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

        // Render the view with the success message
        return View();
    }



    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
