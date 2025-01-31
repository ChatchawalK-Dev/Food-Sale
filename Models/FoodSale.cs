using System;
using System.ComponentModel.DataAnnotations;

namespace Food_Sale.Models
{

    public class FoodSale
    {
        public DateTime OrderDate { get; set; }
        public string Region { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}