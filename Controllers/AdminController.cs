using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Data;
using ExclusiveMVC.Models;

namespace ExclusiveMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Dashboard
        public IActionResult Index()
        {
            return View();
        }

        // View Products
        public IActionResult Products()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Add Product (GET)
        public IActionResult AddProduct()
        {
            return View();
        }

        // Add Product (POST)
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(product);
        }

        // Delete Product
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        // View Orders
        public IActionResult Orders()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
    }
}