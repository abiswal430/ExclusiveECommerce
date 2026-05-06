using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Models;
using ExclusiveMVC.Data;
using System.Linq;

namespace ExclusiveMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ✅ HOME PAGE
        public IActionResult Index()
        {
            try
            {
                // 🔹 STATIC PRODUCTS
                var defaultProducts = new List<Product>
                {
                    new Product
                    {
                        Name = "iPhone 14",
                        Price = 80000,
                        ImageUrl = "/images/iphone14.png"
                    },

                    new Product
                    {
                        Name = "Laptop",
                        Price = 60000,
                        ImageUrl = "/images/laptop.png"
                    },

                    new Product
                    {
                        Name = "Headphones",
                        Price = 3000,
                        ImageUrl = "/images/headphone.png"
                    },

                    new Product
                    {
                        Name = "Gamepad",
                        Price = 1800,
                        ImageUrl = "/images/gamepad.png"
                    },

                    new Product
                    {
                        Name = "Keyboard",
                        Price = 1900,
                        ImageUrl = "/images/keyboard.png"
                    },

                    new Product
                    {
                        Name = "Monitor",
                        Price = 37000,
                        ImageUrl = "/images/monitor.png"
                    },

                    new Product
                    {
                        Name = "CPU Cooler",
                        Price = 5500,
                        ImageUrl = "/images/cooler.png"
                    }
                };

                // 🔹 DATABASE PRODUCTS
                var dbProducts = _context.Products.ToList();

                // 🔹 COMBINE BOTH
                var allProducts = defaultProducts
                    .Concat(dbProducts)
                    .ToList();

                return View(allProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return View(new List<Product>());
            }
        }

        // ✅ PRIVACY PAGE
        public IActionResult Privacy()
        {
            return View();
        }

        // ✅ ABOUT PAGE
        public IActionResult About()
        {
            return View();
        }

        // ✅ CONTACT PAGE (GET)
        public IActionResult Contact()
        {
            return View();
        }

        // ✅ CONTACT FORM (POST)
        [HttpPost]
        public IActionResult Contact(string name, string email, string message)
        {
            try
            {
                // ✅ VALIDATION
                if (string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(message))
                {
                    TempData["error"] = "All fields are required!";
                    return RedirectToAction("Contact");
                }

                // ✅ SAVE MESSAGE TO DATABASE
                var contact = new ContactMessage
                {
                    Name = name,
                    Email = email,
                    Message = message,
                    CreatedAt = DateTime.Now
                };

                _context.ContactMessages.Add(contact);
                _context.SaveChanges();

                // ✅ SUCCESS MESSAGE
                TempData["success"] = "Message sent successfully!";

                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["error"] = "Failed to send message!";

                return RedirectToAction("Contact");
            }
        }

        // ✅ ERROR PAGE
        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
                            HttpContext.TraceIdentifier
            });
        }
    }
}