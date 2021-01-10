using System;


namespace Domain.Models.HistorycalPrices
{
    public class HistorycalPrice 
    {
        public DateTime DateOfPrice { get; private set; }
        public double PriceOfThatDay { get; private set; }
        
        public HistorycalPrice(double priceOfThatDay )
        {
            DateOfPrice = new DateTime();
            PriceOfThatDay = priceOfThatDay;
        }
    }
}