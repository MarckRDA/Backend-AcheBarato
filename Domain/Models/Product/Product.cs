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
        public string ImgLink { get; set; }
        public string LinkRedirectShop { get; set; }
        public Cathegory Cathegory { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<HistorycalPrice> HistorycalPrices { get; set; }
        public string[] Tags { get; set; }

        public Product(string name, string pdIdMLB, double price, Cathegory cathegory)
        {
            Id = Guid.NewGuid();
            ProductIdMLB = pdIdMLB;
            Name = name;
            Price = price;
            Cathegory = cathegory;
        }
    }
}