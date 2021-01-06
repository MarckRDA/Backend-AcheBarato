using System.Collections.Generic;


namespace Domain.Models.Products
{
    public class ProductDTO
    {
        public string ProductIdMLB { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImgLink { get; set; }
        public List<Picture> Pictures { get; set; }
        public string LinkRedirectShop { get; set; }
        public string Cathegory { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<HistorycalPrice> HistorycalPrices { get; set; }
    }
}