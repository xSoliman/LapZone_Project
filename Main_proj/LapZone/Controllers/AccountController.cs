using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Text;

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
    public IActionResult Login(User user)
    {
        HttpContext.Session.Clear();

        var authentUser = _db.Users
            .Where(u => u.Email == user.Email)
            .FirstOrDefault();

        if (authentUser != null && authentUser.PasswordHash == HashPassword(user.PasswordHash))
        {
            HttpContext.Session.SetInt32("UserId", authentUser.UserId); 
            HttpContext.Session.SetInt32("RoleId", authentUser.RoleId);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewData["Wrong"] = "Wrong Email or Password";
            return View(user);
        }
    }
    public IActionResult Register()

    {
        HttpContext.Session.Clear();

        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User user)
    {
        HttpContext.Session.Clear();

        if (string.IsNullOrEmpty(user.FullName)
            || string.IsNullOrEmpty(user.Email)
            || string.IsNullOrEmpty(user.PasswordHash)
            || string.IsNullOrEmpty(user.PhoneNumber)
        )
        {
            ModelState.AddModelError("", "All fields are required.");
            return View(user);
        }

        var isEmailExist = _db.Users.Any(x => x.Email == user.Email);
        if (isEmailExist)
        {
            ModelState.AddModelError("Email", "Email is already exists");
            return View();
        }

        user.PasswordHash = HashPassword(user.PasswordHash);

        string fileName = string.Empty;
        if (user.clientFile != null)
        {
            string upload = Path.Combine(_host.WebRootPath, "Images/Users");

            // Generate a unique file name 
            string uniqueFileName = $"{Guid.NewGuid().ToString()}-{DateTime.Now.Ticks}-{user.clientFile.FileName}";
            string fullPath = Path.Combine(upload, uniqueFileName);

            user.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
            user.ImagePath = uniqueFileName;
        }

        user.RoleId = 2;
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
