using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;
using Microsoft.EntityFrameworkCore;
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

        // 🔁 Redirect
        public IActionResult Checkout()
        {
            return RedirectToAction("Checkout", "Cart");
        }

        // ✅ PLACE ORDER (FIXED)
        [HttpPost]
        public IActionResult PlaceOrder(string name, string phone, string address,
                                       string state, string city, string pincode, string paymentMethod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
                {
                    TempData["error"] = "Name and Phone are required!";
                    return RedirectToAction("Checkout", "Cart");
                }

                var cartItems = _context.Cart
                    .Where(x => !x.IsSaved)
                    .ToList();

                if (!cartItems.Any())
                {
                    TempData["error"] = "Cart is empty!";
                    return RedirectToAction("Index", "Cart");
                }

                decimal total = cartItems.Sum(x => x.Price * x.Quantity);

                // ✅ FIXED ORDER CREATION
                var order = new Order
                {
                    Name = name,
                    Phone = phone,
                    Address = $"{address}, {city}, {state} - {pincode}",
                    TotalAmount = total,

                    // 🔥 IMPORTANT FIX
                    Status = "Placed",

                    // ✅ store payment method separately
                    PaymentMethod = paymentMethod,

                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                };

                foreach (var item in cartItems)
                {
                    order.Items.Add(new OrderItem
                    {
                        ProductName = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                }

                _context.Orders.Add(order);
                _context.Cart.RemoveRange(cartItems);

                _context.SaveChanges();

                HttpContext.Session.SetInt32("CartCount", 0);

                TempData["success"] = "Order placed successfully!";
                return RedirectToAction("History");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                TempData["error"] = "Something went wrong!";
                return RedirectToAction("Checkout", "Cart");
            }
        }

        // 📜 HISTORY
        public IActionResult History()
        {
            var orders = _context.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        // ❌ CANCEL
        public IActionResult Cancel(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order != null && order.Status == "Placed")
            {
                order.Status = "Cancelled";
                _context.SaveChanges();
                TempData["success"] = "Order cancelled!";
            }
            else
            {
                TempData["error"] = "Cannot cancel this order!";
            }

            return RedirectToAction("History");
        }

        // 🚚 SHIPPED
        public IActionResult MarkShipped(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order != null && order.Status == "Placed")
            {
                order.Status = "Shipped";
                _context.SaveChanges();
                TempData["success"] = "Order shipped!";
            }
            else
            {
                TempData["error"] = "Cannot ship this order!";
            }

            return RedirectToAction("History");
        }

        // 📦 DELIVERED
        public IActionResult MarkDelivered(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order != null && order.Status == "Shipped")
            {
                order.Status = "Delivered";
                _context.SaveChanges();
                TempData["success"] = "Order delivered!";
            }
            else
            {
                TempData["error"] = "Order must be shipped first!";
            }

            return RedirectToAction("History");
        }

        // 🧾 INVOICE
        public IActionResult Invoice(int id)
        {
            var order = _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            return View(order);
        }
    }
}