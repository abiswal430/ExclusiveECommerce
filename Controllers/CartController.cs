using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;
using System.Linq;

namespace ExclusiveMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // 🛒 VIEW CART
        public IActionResult Index()
        {
            var items = _context.Cart
                .Where(x => !x.IsSaved)
                .ToList();

            var savedItems = _context.Cart
                .Where(x => x.IsSaved)
                .ToList();

            ViewBag.SavedItems = savedItems;

            return View(items);
        }

        // ➕ ADD TO CART
        public IActionResult Add(string name, decimal price, string image)
        {
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Index", "Home");

            var item = _context.Cart
                .FirstOrDefault(x => x.Name == name && !x.IsSaved);

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
                    ImageUrl = image ?? "",
                    IsSaved = false
                });
            }

            _context.SaveChanges();
            UpdateCartCount();

            return RedirectToAction("Index", "Home");
        }

        // ❌ REMOVE ITEM
        public IActionResult Remove(int id)
        {
            var item = _context.Cart.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _context.Cart.Remove(item);
                _context.SaveChanges();
            }

            UpdateCartCount();
            return RedirectToAction("Index");
        }

        // ➕ INCREASE QUANTITY
        public IActionResult Increase(int id)
        {
            var item = _context.Cart
                .FirstOrDefault(x => x.Id == id && !x.IsSaved);

            if (item != null)
            {
                item.Quantity++;
                _context.SaveChanges();
            }

            UpdateCartCount();
            return RedirectToAction("Index");
        }

        // ➖ DECREASE QUANTITY
        public IActionResult Decrease(int id)
        {
            var item = _context.Cart
                .FirstOrDefault(x => x.Id == id && !x.IsSaved);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    _context.Cart.Remove(item);
                }

                _context.SaveChanges();
            }

            UpdateCartCount();
            return RedirectToAction("Index");
        }

        // 🔄 UPDATE QUANTITY
        [HttpPost]
        public IActionResult UpdateQty(int id, int qty)
        {
            var item = _context.Cart
                .FirstOrDefault(x => x.Id == id && !x.IsSaved);

            if (item != null && qty > 0)
            {
                item.Quantity = qty;
                _context.SaveChanges();
            }

            UpdateCartCount();
            return RedirectToAction("Index");
        }

        // 💾 SAVE FOR LATER
        public IActionResult SaveForLater(int id)
        {
            var item = _context.Cart.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.IsSaved = true;
                _context.SaveChanges();
            }

            UpdateCartCount();
            return RedirectToAction("Index");
        }

        // 🔙 MOVE BACK TO CART
        public IActionResult MoveToCart(int id)
        {
            var item = _context.Cart.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.IsSaved = false;

                // if already exists in cart, merge quantity
                var existing = _context.Cart
                    .FirstOrDefault(x => x.Name == item.Name && !x.IsSaved && x.Id != id);

                if (existing != null)
                {
                    existing.Quantity += item.Quantity;
                    _context.Cart.Remove(item);
                }

                _context.SaveChanges();
            }

            UpdateCartCount();
            return RedirectToAction("Index");
        }

        // 🧾 CHECKOUT
        public IActionResult Checkout()
        {
            var items = _context.Cart
                .Where(x => !x.IsSaved)
                .ToList();

            return View(items);
        }

        // 🔄 UPDATE SESSION COUNT
        private void UpdateCartCount()
        {
            int count = _context.Cart
                .Where(x => !x.IsSaved)
                .Sum(x => x.Quantity);

            HttpContext.Session.SetInt32("CartCount", count);
        }
    }
}