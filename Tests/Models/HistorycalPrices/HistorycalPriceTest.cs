using Domain.Models.HistorycalPrices;
using Xunit;

namespace Tests.HistorycalPrices
{
    public class HistorycalPriceTest
    {
        [Fact]
        public void Create_HistorycalPrice()
        {
            //Given, When
            var historycalPrice = new HistorycalPrice(11.28, "18/01/2021");
            
            //Then
            Assert.Equal("18/01/2021", historycalPrice.DateOfPrice);
            Assert.Equal(11.28 , historycalPrice.PriceOfThatDay);
            Assert.NotNull(historycalPrice);
        }
    }
}