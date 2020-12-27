using System;
using System.Collections.Generic;
using Domain.Models.Cathegories;
using Domain.Models.Entities;

namespace Domain.Models.Products
{
    public class Product : Entity
    {
        public string ProductIdMLB { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ThumbImgLink { get; set; }
        public string LinkRedirectShop { get; set; }
        public Cathegory Cathegory { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<HistorycalPrice> HistorycalPrices { get; set; }
        public List<String> Pictures { get; set; }
        public string[] Tags { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
        }
    }
}