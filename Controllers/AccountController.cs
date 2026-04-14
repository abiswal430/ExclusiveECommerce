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

        // REGISTER PAGE
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
          _context.Users.Add(user);
          _context.SaveChanges();

          return RedirectToAction("Login", "Account");
        }

        // LOGIN PAGE
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

        return RedirectToAction("Index", "Home"); // ✅ FIX
    }

    ViewBag.Error = "Invalid Username or Password";
    return View();
}

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}