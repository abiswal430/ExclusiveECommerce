namespace ExclusiveMVC.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }   // ✅ FIX NULLABLE

        public string? ProductName { get; set; }   // ✅ FIX
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}