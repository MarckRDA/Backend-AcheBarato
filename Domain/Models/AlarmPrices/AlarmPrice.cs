using Domain.Models.Products;

namespace Domain.Models.AlarmPrices
{
    public class AlarmPrice 
    {
        public Product ProductToMonitor { get; private set; } 
        public double WishPrice { get; private set; }

        public AlarmPrice(Product productToMonitor, double wishPrice)
        {
            ProductToMonitor = productToMonitor;
            WishPrice = wishPrice;
        }
    
    }
}