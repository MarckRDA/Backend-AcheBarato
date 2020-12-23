using System;
using System.Collections.Generic;
using Domain.Models.Products;

namespace Domain.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Product> WishListProducts { get; set; }
    }
}