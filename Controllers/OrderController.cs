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

        // 🔥 REDIRECT CHECKOUT TO CART (FIXED ISSUE)
        public IActionResult Checkout()
        {
            return RedirectToAction("Checkout", "Cart");
        }

        // ✅ PLACE ORDER
        [HttpPost]
        public IActionResult PlaceOrder(IFormCollection form)
        {
            try
            {
                // 🔥 GET FORM DATA SAFELY
                string name = form["name"].ToString();
                string phone = form["phone"].ToString();
                string address = form["address"].ToString();
                string state = form["state"].ToString();
                string city = form["city"].ToString();
                string pincode = form["pincode"].ToString();

                // 🚫 VALIDATION
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
                {
                    TempData["error"] = "Name and Phone are required!";
                    return RedirectToAction("Checkout", "Cart");
                }

                // 🛒 GET ACTIVE CART ITEMS
                var cartItems = _context.Cart
                    .Where(x => !x.IsSaved)
                    .ToList();

                if (!cartItems.Any())
                {
                    TempData["error"] = "Cart is empty!";
                    return RedirectToAction("Index", "Cart");
                }

                // 💰 CALCULATE TOTAL
                decimal total = cartItems.Sum(x => x.Price * x.Quantity);

                // 📍 FULL ADDRESS
                string fullAddress = $"{address}, {city}, {state} - {pincode}";

                // 📦 CREATE ORDER
                var order = new Order
                {
                    CustomerName = name,
                    Phone = phone,
                    Address = fullAddress,
                    TotalAmount = total,
                    Status = "Placed",
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                };

                // 📋 ADD ORDER ITEMS
                foreach (var item in cartItems)
                {
                    order.Items.Add(new OrderItem
                    {
                        ProductName = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                }

                // 💾 SAVE ORDER
                _context.Orders.Add(order);

                // 🧹 CLEAR CART
                _context.Cart.RemoveRange(cartItems);

                _context.SaveChanges();

                // 🔄 RESET CART COUNT
                HttpContext.Session.SetInt32("CartCount", 0);

                TempData["success"] = "Order placed successfully!";

                return RedirectToAction("History");
            }
            catch (Exception)
            {
                TempData["error"] = "Something went wrong!";
                return RedirectToAction("Checkout", "Cart");
            }
        }

        // 📜 ORDER HISTORY
        public IActionResult History()
        {
            var orders = _context.Orders
                .Include(o => o.Items)
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
                TempData["success"] = "Order cancelled!";
            }
            else
            {
                TempData["error"] = "Cannot cancel this order!";
            }

            return RedirectToAction("History");
        }

        // 🚚 MARK AS SHIPPED
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

        // 🚚 MARK AS DELIVERED
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