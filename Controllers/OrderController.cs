using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;

public class OrderController : Controller
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ SHOW CHECKOUT PAGE
    public IActionResult Checkout()
    {
        return View();
    }

    // ✅ PLACE ORDER
    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();

        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }

    public IActionResult History()
{
    var orders = _context.Orders.ToList();
    return View(orders);
}
}