using System;
using Domain.Models.Entities;

namespace Domain.Models.Products
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public Product Product { get; set; }
        
        public Tag()
        {
            Id = Guid.NewGuid();
        }
    }
}