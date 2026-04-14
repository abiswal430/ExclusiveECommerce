using System.ComponentModel.DataAnnotations;

namespace ExclusiveMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }   // ✅ FIX
        public string? Phone { get; set; }          // ✅ FIX
        public string? Address { get; set; }        // ✅ FIX

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }         // ✅ FIX

        public DateTime OrderDate { get; set; }

        // ✅ RELATION
        public List<OrderItem>? Items { get; set; }
    }
}