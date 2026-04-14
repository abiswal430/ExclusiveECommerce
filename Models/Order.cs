using System;

namespace ExclusiveMVC.Models
{
    public class Order
    {
 public int Id { get; set; }

        public string CustomerName { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Placed";

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}