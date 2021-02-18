namespace Domain.Common
{
    public class QueryParameters 
    {
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = double.MaxValue;
        public string Search { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; } = "";
        public int Limit { get; set; } = 12;

        public QueryParameters() {}

        public QueryParameters(int pageNumber, int limit)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            Limit = limit;
        }

        public bool ValidateValuePrice() => MaxPrice > MinPrice;
    }
}