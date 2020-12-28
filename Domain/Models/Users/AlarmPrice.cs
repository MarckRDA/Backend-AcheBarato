using Domain.Models.Entities;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public class AlarmPrice : Entity
    {
        public Product ProductToMonitor { get; set; } 
        public double WishPrice { get; set; }
    
    }
}