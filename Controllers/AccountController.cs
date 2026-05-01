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

        // ================= REGISTER =================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                // ✅ Validation
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    TempData["error"] = "All fields are required!";
                    return RedirectToAction("Register"); // 🔥 FIX
                }

                // ✅ Check duplicate user
                var exists = _context.Users
                    .FirstOrDefault(x => x.Username == user.Username);

                if (exists != null)
                {
                    TempData["error"] = "Username already exists!";
                    return RedirectToAction("Register"); // 🔥 FIX
                }

                // ✅ Save user
                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["success"] = "Registration successful!";
                return RedirectToAction("Login"); // ✅ SUCCESS FLOW
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["error"] = "Registration failed!";
                return RedirectToAction("Register"); // 🔥 FIX
            }
        }

        // ================= LOGIN =================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("username", user.Username);

                TempData["success"] = "Login successful!";
                return RedirectToAction("Index", "Home"); // ✅ SUCCESS
            }

            TempData["error"] = "Invalid Username or Password";
            return RedirectToAction("Login"); // 🔥 FIX
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["success"] = "Logged out successfully!";
            return RedirectToAction("Login");
        }
    }
}