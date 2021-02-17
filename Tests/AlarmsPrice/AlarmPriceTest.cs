using System;
using Xunit;
using Domain.Models.AlarmPrices;

namespace Tests.AlarmPrices
{
    public class AlarmPriceTest
    {
        [Theory]
        [InlineData(22.5, 22.5)]
        [InlineData(0, 0)]
        [InlineData(63541.3541, 63541.3541)]
        public void IsTheSamePrice_return_true(double price, double productsPrice)
        {
            //Given
            var alarmPrice = new AlarmPrice(Guid.NewGuid(), price);
            
            //When
            var validation = alarmPrice.IsTheSamePrice(productsPrice);
            
            //Then
            Assert.True(validation);
        }

        [Theory]
        [InlineData(26512.5, 22.5)]
        [InlineData(1, 0)]
        [InlineData(631.3541, 63541.3541)]
        public void IsTheSamePrice_return_false(double price, double productsPrice)
        {
            //Given
            var alarmPrice = new AlarmPrice(Guid.NewGuid(), price);
            
            //When
            var validation = alarmPrice.IsTheSamePrice(productsPrice);
            
            //Then
            Assert.False(validation);
        }
    }
}