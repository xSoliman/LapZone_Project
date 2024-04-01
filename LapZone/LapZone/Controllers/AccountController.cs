using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Text;
using System.Text.RegularExpressions;
using LapZone.ViewModels;

namespace LapZone.Controllers;

public class AccountController : Controller
{
    private readonly LapZoneContext _db;
    private readonly IHostingEnvironment _host;

    public AccountController(LapZoneContext db, IHostingEnvironment host)
    {
        _db = db;
        _host = host;

    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        HttpContext.Session.Clear();

        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginRequest user)
    {
        if (ModelState.IsValid)
        {
            var authentUser = _db.Users
                .Where(u => u.Email == user.Email)
                .FirstOrDefault();

            if (authentUser != null &&
                authentUser.PasswordHash == HashPassword(user.PasswordHash))
            {
                HttpContext.Session.SetInt32("UserId", authentUser.UserId);
                HttpContext.Session.SetInt32("RoleId", authentUser.RoleId);

                return RedirectToAction("Index", "Home");
            }
            else
            {

                TempData["Wrong"] = "Wrong Email or Password";
                return View(user);
            }
        }
        else
        { 

            return View(user);
        }
    }

    public IActionResult Register()

    {
        HttpContext.Session.Clear();

        return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(SignUpRequest signUpRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(signUpRequest);
        }

        var isEmailExist = _db.Users.Any(x => x.Email == signUpRequest.Email);
        if (isEmailExist)
        {
            ModelState.AddModelError("Email", "Email is already exists");
            return View(signUpRequest);
        }

        // Create a User instance and map properties from SignUpRequest
        var user = new User
        {
            FullName = signUpRequest.FullName,
            Email = signUpRequest.Email,
            PasswordHash = HashPassword(signUpRequest.PasswordHash),
            PhoneNumber = signUpRequest.PhoneNumber,
            ImagePath = signUpRequest.ImagePath,
            RoleId = 3
        };


        // Validate image file type
        if (signUpRequest.clientFile != null)
        {
            if (!signUpRequest.clientFile.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("clientFile", "Invalid file type. Please upload an image.");
                return View(signUpRequest);
            }

        }
            // Save the image file if provided
            if (signUpRequest.clientFile != null)
        {
            string upload = Path.Combine(_host.WebRootPath, "Images/Users");

            string uniqueFileName = $"{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}-{signUpRequest.clientFile.FileName}";
            string fullPath = Path.Combine(upload, uniqueFileName);

            signUpRequest.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
            user.ImagePath = uniqueFileName;
        }
        else
        {
            // Generate a unique filename for the default photo
            string defaultFileName = $"default-{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}.jpg";
            string defaultPhotoPath = Path.Combine(_host.WebRootPath, "Images/Users", defaultFileName);

            // Copy the default photo to the unique filename
            System.IO.File.Copy(Path.Combine(_host.WebRootPath, "Images/Users/default.png"), defaultPhotoPath);

            // Set the user.ImagePath to the unique filename
            user.ImagePath = defaultFileName;
        }

        _db.Users.Add(user);
        _db.SaveChanges();

        return RedirectToAction("Login", "Account");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
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
