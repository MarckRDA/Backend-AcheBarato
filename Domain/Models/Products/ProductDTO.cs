using System.Collections.Generic;
using Domain.Models.Cathegories;
using Domain.Models.Descriptions;
using Domain.Models.HistorycalPrices;

namespace Domain.Models.Products
{
    public class ProductDTO
    {
        public string ProductIdMLB { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImgLink { get; set; }
        public List<string> Pictures { get; set; }
        public string LinkRedirectShop { get; set; }
        public Cathegory Cathegory { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<HistorycalPrice> HistorycalPrices { get; set; }
    }
}