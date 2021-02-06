namespace Domain.Models.Products
{
    public class ProductQueryParameters 
    {
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = double.MaxValue;
        public string Search { get; set; } = "";
        public int PageNumber { get; set; }
        public string OrderBy { get; set; } = "";
        public int PageSize { get; set; } = 10;

        public bool ValidateValuePrice() => MaxPrice > MinPrice;
    }
}