using System;
using System.Collections.Generic;
using Domain.Attributes;
using Domain.Models.Cathegories;
using Domain.Models.Descriptions;
using Domain.Models.HistorycalPrices;

namespace Domain.Models.Products

{
    [BsonCollection("Products")]
    public class Product 
    {
        public Guid id_product { get; private set; } = new Guid();
        public string MLBId { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string ThumbImgLink { get; private set; }
        public string LinkRedirectShop { get; private set; }
        public Cathegory Cathegory { get; private set; }
        public List<Description> Descriptions {get; private set;}
        public List<HistorycalPrice> HistorycalṔrices {get; private set;}
        public List<string> Pictures {get; private set;}
        public string[] Tag {get; private set;}
        public bool isTrending {get; set;}

        public Product(string name, string productIdMLB, double price, string thumbImgLink, string linkRedirectShop, Cathegory cathegory, string[] tag)
        {
            id_product = Guid.NewGuid();
            Name = name;
            MLBId = productIdMLB;
            Price = price;
            ThumbImgLink = thumbImgLink;
            LinkRedirectShop = linkRedirectShop;
            Cathegory = cathegory;
            Descriptions = new List<Description>();
            HistorycalṔrices = new List<HistorycalPrice>();
            Pictures = new List<string>();
            Tag = tag;
        }

        public void AddDescription(Description description)
        {
            Descriptions.Add(description);
        }

        public void AddPicture(string linkPicture)
        {
            Pictures.Add(linkPicture);
        }

        public void AddHistoricalPrice(HistorycalPrice hpItem)
        {
            HistorycalṔrices.Add(hpItem);
        }

        public void UpdateProductPrice(double newPrice)
        {
            Price = newPrice;
        }
        
        
    }
}