using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;
using System.Linq;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var items = _context.Cart.ToList();
        return View(items);
    }

    public IActionResult Add(string name, decimal price, string image)
    {
        var item = _context.Cart.FirstOrDefault(x => x.Name == name);

        if (item != null)
        {
            item.Quantity++;
        }
        else
        {
            _context.Cart.Add(new Cart
            {
                Name = "iPhone 14",
                Price = 80000,
                Quantity = 1,
                ImageUrl = "/images/iphone14.png"
            });

            _context.Cart.Add(new Cart
            {
                Name = "Gamepad",
                Price = 2000,
                Quantity = 1,
                ImageUrl = "/images/gamepad.png"
            });

            _context.Cart.Add(new Cart
            {
                Name = "Headphones",
                Price = 3000,
                Quantity = 1,
                ImageUrl = "/images/headphone.png"
            });

                _context.Cart.Add(new Cart
                {
                    Name = "Keyboard",
                    Price = 1900,
                    Quantity = 1,
                    ImageUrl = "/images/keyboard.png"
                });
    
                _context.Cart.Add(new Cart
                {
                    Name = "Monitor",
                    Price = 37000,
                    Quantity = 1,
                    ImageUrl = "/images/monitor.png"
                });

                _context.Cart.Add(new Cart
                {
                    Name = "Laptop",
                    Price = 60000,
                    Quantity = 1,
                    ImageUrl = "/images/laptop.png"
                });
        }

        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    

    public IActionResult Checkout()
    {
        var items = _context.Cart.ToList();
        return View(items);
    }
}