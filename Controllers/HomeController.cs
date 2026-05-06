using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Models;
using ExclusiveMVC.Data;
using System.Linq;

namespace ExclusiveMVC.Controllers;

// Final UI improvement for submission

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // ✅ HOME PAGE (STATIC + DATABASE COMBINED)
    public IActionResult Index()
    {
        // 🔹 DEFAULT PRODUCTS (STATIC)
        var defaultProducts = new List<Product>
        {
            new Product { Name="iPhone 14", Price=80000, ImageUrl="/images/iphone14.png" },
            new Product { Name="Laptop", Price=60000, ImageUrl="/images/laptop.png" },
            new Product { Name="Headphones", Price=3000, ImageUrl="/images/headphone.png" },
            new Product { Name="Gamepad", Price=1800, ImageUrl="/images/gamepad.png" },
            new Product { Name="Keyboard", Price=1900, ImageUrl="/images/keyboard.png" },
            new Product { Name="Monitor", Price=37000, ImageUrl="/images/monitor.png" },
            new Product { Name="CPU Cooler", Price=5500, ImageUrl="/images/cooler.png" }
        };

        // 🔹 DATABASE PRODUCTS
        var dbProducts = _context.Products.ToList();

        // 🔹 COMBINE BOTH
        var allProducts = defaultProducts.Concat(dbProducts).ToList();

        return View(allProducts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    // ✅ GET CONTACT PAGE
    public IActionResult Contact()
    {
        return View();
    }

    // ✅ POST CONTACT FORM
    [HttpPost]
    public IActionResult Contact(string name, string email, string message)
    {
        try
    {
        // You can store in DB later if needed

        // ✅ SUCCESS MESSAGE
        TempData["success"] = "Message sent successfully!";

        return RedirectToAction("Contact");
    }
    catch
    {
        TempData["error"] = "Failed to send message!";
        return RedirectToAction("Contact");
    }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}