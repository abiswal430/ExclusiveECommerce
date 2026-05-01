using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ExclusiveMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ REGISTER PAGE (GET)
        public IActionResult Register()
        {
            return View();
        }

        // ✅ REGISTER (POST)
        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    TempData["error"] = "All fields are required!";
                    return View(user);
                }

                // 🔥 Check if user already exists
                var exists = _context.Users.FirstOrDefault(x => x.Username == user.Username);

                if (exists != null)
                {
                    TempData["error"] = "Username already exists!";
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                // ✅ SUCCESS MESSAGE
                TempData["success"] = "Registration successful!";

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["error"] = "Registration failed!";
                return View(user);
            }
        }

        // ✅ LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }

        // ✅ LOGIN POST
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("username", user.Username);

                TempData["success"] = "Login successful!";

                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Invalid Username or Password";
            return View();
        }

        // ✅ LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["success"] = "Logged out successfully!";
            return RedirectToAction("Login");
        }
    }
}