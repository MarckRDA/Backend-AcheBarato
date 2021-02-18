using Moq;
using Domain.Common;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Tests.Mocks;
using Xunit;
using System.Collections.Generic;

namespace Tests.Products
{
    public class ProductsRepositoryTest : ProductsTestMethods
    {
        [Fact]
        public void Add_is_valid()
        {
            var products = ListProductGenerator();
            //Given
            ProductRepository
                .Setup(p => p.add(products[0]));
            
            //When
            var mongoRepository = MockedMongoDBGenerator();
            
            //Then
            mongoRepository.Verify(p => p.add(ProductGenerator()), Times.Never());
        }
    }
}