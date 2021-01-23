namespace Domain.Models.HistorycalPrices
{
    public class HistorycalPrice 
    {
        public string DateOfPrice { get; private set; }
        public double PriceOfThatDay { get; private set; }
        public HistorycalPrice(double priceOfThatDay, string date )
        {
            DateOfPrice = date;
            PriceOfThatDay = priceOfThatDay;
        }
    }
}