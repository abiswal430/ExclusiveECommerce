using Microsoft.AspNetCore.Mvc;
using ExclusiveMVC.Models;
using System.Text.Json;

namespace ExclusiveMVC.Controllers
{
    public class ApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Products()
        {
            var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");

            if (!response.IsSuccessStatusCode)
                return View(new List<ApiProduct>());

            var json = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<ApiProduct>>(json);

            return View(products);
        }
    }
}