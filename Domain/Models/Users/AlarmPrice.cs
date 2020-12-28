using System;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public class AlarmPrice
    {
        public Guid AlarmPriceId { get; set; }
        public Product ProductToMonitor { get; set; } 
        public double WishPrice { get; set; }
    
    }
}