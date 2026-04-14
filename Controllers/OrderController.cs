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

        // ✅ PLACE ORDER
        [HttpPost]
        public IActionResult PlaceOrder(string name, string phone, string address,
                                       string state, string city, string pincode)
        {
            try
            {
                var cartItems = _context.Cart.ToList();

                if (!cartItems.Any())
                {
                    TempData["error"] = "Cart is empty!";
                    return RedirectToAction("Index", "Cart");
                }

                decimal total = cartItems.Sum(x => x.Price * x.Quantity);

                string fullAddress = $"{address}, {city}, {state} - {pincode}";

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
            catch (Exception)
            {
                TempData["error"] = "Something went wrong!";
                return RedirectToAction("Checkout");
            }
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

                TempData["success"] = "Order cancelled!";
            }
            else
            {
                TempData["error"] = "Cannot cancel this order!";
            }

            return RedirectToAction("History");
        }

        // 🚚 MARK AS SHIPPED (NEW 🔥)
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
    }
}