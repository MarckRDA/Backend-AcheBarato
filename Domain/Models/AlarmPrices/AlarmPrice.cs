using System;

namespace Domain.Models.AlarmPrices
{
    public class AlarmPrice 
    {
        public Guid ProductToMonitorId { get; private set; } = new Guid();
        public double WishPrice { get; private set; }
        
        public AlarmPrice(Guid productToMonitorId, double wishPrice)
        {
            ProductToMonitorId = productToMonitorId;
            WishPrice = wishPrice;
        }

        public bool IsTheSamePrice(double productsPrice) => productsPrice == WishPrice;
    }
}