using System;
using System.Collections.Generic;
using Domain.Attributes;
using Domain.Models.Descriptions;
using Domain.Models.HistorycalPrices;

namespace Domain.Models.Products

{
    [BsonCollection("Products")]
    public class Product 
    {
        public string Id { get; private set; }
        public string ProductIdMLB { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string ThumbImgLink { get; private set; }
        public string LinkRedirectShop { get; private set; }
        public string Cathegory { get; private set; }
        public List<Description> Descriptions {get; private set;}
        public List<HistorycalPrice> HistorycalṔrices {get; private set;}
        public List<string> Pictures {get; private set;}
        public string[] Tag {get; private set;}

        public Product(string name, string productIdMLB, double price, string thumbImgLink, string linkRedirectShop, string cathegory, string[] tag)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            ProductIdMLB = productIdMLB;
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
    }
}