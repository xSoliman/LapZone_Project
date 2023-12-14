using LapZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using System.Linq;
using System.Dynamic;

namespace LapZone.Controllers;

public class AccountController : Controller
{
    private readonly LapZoneContext _db;
    public AccountController(LapZoneContext db)
    {
        _db = db;

    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
        var authentUser = _db.Users
         .Where(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash)
         .FirstOrDefault();
      

        if (authentUser != null)
        {
            HttpContext.Session.SetString("Email", user.Email);
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
        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User user)
    {
        if (string.IsNullOrEmpty(user.FullName)
    || string.IsNullOrEmpty(user.Email)
    || string.IsNullOrEmpty(user.PasswordHash)
    || string.IsNullOrEmpty(user.PhoneNumber)
    || string.IsNullOrEmpty(user.Photo))
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
        user.RoleId = 2;
        _db.Users.Add(user);
        _db.SaveChanges();

        return RedirectToAction("Login", "Account");
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

}
