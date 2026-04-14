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

    // ❌ REMOVE ITEM FROM CART
    public IActionResult Remove(string name)
    {
    var item = _context.Cart.FirstOrDefault(x => x.Name == name);

    if (item != null)
    {
        _context.Cart.Remove(item);
        _context.SaveChanges();
    }

    // 🔥 UPDATE COUNT AGAIN
    int count = _context.Cart.Sum(x => x.Quantity);
    HttpContext.Session.SetInt32("CartCount", count);

    return RedirectToAction("Index");
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
            Name = name,
            Price = price,
            Quantity = 1,
            ImageUrl = image
        });
    }

    _context.SaveChanges();

    // 🔥 UPDATE CART COUNT IN SESSION
    int count = _context.Cart.Sum(x => x.Quantity);
    HttpContext.Session.SetInt32("CartCount", count);

    return RedirectToAction("Index", "Home");
    }

    public IActionResult Checkout()
    {
        var items = _context.Cart.ToList();
        return View(items);
    }
}