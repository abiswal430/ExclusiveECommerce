using System;
using System.Collections.Generic;

namespace ExclusiveMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        // ✅ ADD THIS (FIX)
        public string? PaymentMethod { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItem>? Items { get; set; }
    }
}