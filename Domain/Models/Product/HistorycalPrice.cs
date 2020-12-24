using System;
namespace Domain.Models.Products
{
    public class HistorycalPrice
    {
        public Guid HistorycalPriceId { get; set; }
        public DateTime DateOfPrice { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
    }
}