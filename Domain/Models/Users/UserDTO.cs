using System;
using System.Collections.Generic;
using Domain.Models.AlarmPrices;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public class UserDTO
    {
        public Guid UserId { get; set; } = new Guid();
        public string Name { get; set; }
        public List<Product> WishListProducts {get; set;} = new List<Product>();
        public List<string> SearchTag { get; set; } = new List<string>();
        public List<AlarmPrice> WishProductsAlarmPrices {get; set;} = new List<AlarmPrice>();
    }    
}