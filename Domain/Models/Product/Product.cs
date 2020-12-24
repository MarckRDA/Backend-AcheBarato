using Domain.Models.Cathegories;

namespace Domain.Models.Products
{
    public class Product
    {
        public string ProductIdMLB { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImgLink { get; set; }
        public string LinkRedirectShop { get; set; }
        public Cathegory cathegory { get; set; }
        public HistorycalPrice HistorycalPrices { get; set; }
    }
}