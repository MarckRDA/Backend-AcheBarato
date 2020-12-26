using System;

namespace Domain.Models.Products
{
    public class AlarmPrice
    {
        public Guid AlarmPriceId { get; set; }
        public Product ProductToMonitor { get; set; } 
        public double WishPrice { get; set; }
    
    }
}