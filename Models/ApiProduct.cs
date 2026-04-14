namespace ExclusiveMVC.Models
{
    public class ApiProduct
    {
        public int id { get; set; }

        public string? title { get; set; }   // ✅ FIX
        public decimal price { get; set; }
        public string? image { get; set; }   // ✅ FIX
    }
}