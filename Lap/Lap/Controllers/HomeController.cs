using Lap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Lap.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly LapContext _dbContext;
        private static string _customer { get; set; }

        public HomeController(LapContext dbContext)
        {
            _dbContext = dbContext;
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
        public IActionResult Login(Customer customer)
        {
            /*            if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password))
                        {
                            ViewData["Wrong"] = "Email and password are required.";
                            return View();
                        }*/

            if (DataModel.UserLogin(customer))
            {
                _customer = customer.Email;
                return View("Index");
            }
            else
            {
                ViewData["Wrong"] = "Wrong Email or Password";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName)
       || string.IsNullOrEmpty(customer.LastName)
       || string.IsNullOrEmpty(customer.Email)
       || string.IsNullOrEmpty(customer.Password)
       || string.IsNullOrEmpty(customer.Phone)
       || string.IsNullOrEmpty(customer.Photo)){
                ModelState.AddModelError("", "All fields are required.");
                return View(customer);
            }

            var isEmailExist = _dbContext.Customers.Any(x => x.Email == customer.Email);
            if (isEmailExist)
            {
                ModelState.AddModelError("Email", "Email is already exists");
                return View();
            }

            DataModel.AddUser(customer);
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
