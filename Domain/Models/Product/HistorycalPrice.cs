using System;
using Domain.Models.Entities;

namespace Domain.Models.Products
{
    public class HistorycalPrice : Entity
    {
        public DateTime DateOfPrice { get; private set; }
        public Product Product { get; private set; }
        public double PriceOfThatDay { get; set; }

        public HistorycalPrice(Product product, DateTime dateOfPrice, double priceOfThatDay )
        {
            Id = Guid.NewGuid();
            Product = product;
            DateOfPrice = new DateTime();
            PriceOfThatDay = priceOfThatDay;
        }
    }
}