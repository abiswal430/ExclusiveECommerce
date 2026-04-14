using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Models;

namespace ExclusiveMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
    var products = new List<Product>
    {
        new Product { Name="iPhone 14", Price=80000, ImageUrl="/images/iphone14.png" },
        new Product { Name="Laptop", Price=60000, ImageUrl="/images/laptop.png" },
        new Product { Name="Headphones", Price=3000, ImageUrl="/images/headphone.png" },
        new Product { Name="Gamepad", Price=1800, ImageUrl="/images/gamepad.png" },
        new Product { Name="Keyboard", Price=1900, ImageUrl="/images/keyboard.png" },
        new Product { Name="Monitor", Price=37000, ImageUrl="/images/monitor.png" },
        new Product{ Name="CPU Cooler", Price=5500, ImageUrl="/images/cooler.png" },
    };

    return View(products);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
{
    return View();
}

public IActionResult Contact()
{
    return View();
}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
