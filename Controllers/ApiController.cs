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

        // 🌐 FETCH PRODUCTS FROM FAKESTORE API
        public async Task<IActionResult> Products()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");

                if (!response.IsSuccessStatusCode)
                {
                    return View(new List<ApiProduct>());
                }

                var data = await response.Content.ReadAsStringAsync();

                var products = JsonSerializer.Deserialize<List<ApiProduct>>(data,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(products ?? new List<ApiProduct>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Prevent crash
                return View(new List<ApiProduct>());
            }
        }
    }
}