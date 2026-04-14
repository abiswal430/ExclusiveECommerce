using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;
using System.Linq;

namespace ExclusiveMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ CHECKOUT PAGE
        public IActionResult Checkout()
        {
            var cartItems = _context.Cart.ToList();
            return View(cartItems);
        }

        // ✅ PLACE ORDER (UPDATED WITH FULL ADDRESS)
        [HttpPost]
        public IActionResult PlaceOrder(string name, string phone, string address,
                                       string state, string city, string pincode)
        {
            var cartItems = _context.Cart.ToList();

            // 🚫 Prevent empty order
            if (!cartItems.Any())
                return RedirectToAction("Index", "Cart");

            // ✅ Calculate total
            decimal total = cartItems.Sum(x => x.Price * x.Quantity);

            // ✅ Combine full address
            string fullAddress = $"{address}, {city}, {state} - {pincode}";

            // ✅ Save order
            var order = new Order
            {
                CustomerName = name,
                Phone = phone,
                Address = fullAddress,
                TotalAmount = total,
                Status = "Placed", // 🔥 important for tracking
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);

            // 🧹 Clear cart
            _context.Cart.RemoveRange(cartItems);

            _context.SaveChanges();

            // 🔴 Reset cart count
            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("Success");
        }

        // ✅ SUCCESS PAGE
        public IActionResult Success()
        {
            return View();
        }

        // ✅ ORDER HISTORY
        public IActionResult History()
        {
            var orders = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        // ❌ CANCEL ORDER
        public IActionResult Cancel(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order != null && order.Status == "Placed")
            {
                order.Status = "Cancelled";
                _context.SaveChanges();
            }

            return RedirectToAction("History");
        }

        // 🚚 MARK AS DELIVERED (Admin Feature)
        public IActionResult MarkDelivered(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order != null)
            {
                order.Status = "Delivered";
                _context.SaveChanges();
            }

            return RedirectToAction("History");
        }
    }
}