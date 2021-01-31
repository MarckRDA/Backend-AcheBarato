using System;
using System.Collections.Generic;
using Domain.Models.AlarmPrices;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Product> WishListProducts {get; set;} 
        public List<AlarmPrice> WishProductsAlarmPrices {get; set;}
    }    
}