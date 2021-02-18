using Domain.Common;
using Moq;
using Xunit;

namespace Tests.QuerysParameters
{
    public class QueryParametersTest
    {
        [Fact]
        public void QueryParameters_valid()
        {
            //Given
            var query = new QueryParameters();
            
            //When
            var validation  = query.ValidateValuePrice();
            
            //Then
            Assert.True(validation);
            Assert.NotNull(query);
        }

        [Fact]
        public void QueryParameters_invalid()
        {
            //Given
            var query = new QueryParameters(It.IsAny<int>(), It.IsAny<int>());
            query.MaxPrice = query.MinPrice;
            
            //When
            var validation  = query.ValidateValuePrice();
            
            //Then
            Assert.False(validation);
            Assert.NotNull(query);
        }
    }
}