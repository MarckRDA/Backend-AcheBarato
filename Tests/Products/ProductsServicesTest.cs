using Moq;
using Domain.Common;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Xunit;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tests.Products
{
    public class ProductsServicesTest : ProductsTestMethods
    {
        [Fact]
        public void GetProductsByCategory_starts_with_MLB()
        {
            //Given
            var product = ProductGenerator();
            var query = QueryGenerator(2, 11);
            query.Search = "MLB";
            ProductRepository.Setup(r => r.GetProductsByCategories(query)).Returns(ListProductGenerator());

            //When
            var returnedProducts = ProductServices.GetAllProduct(query);
            // Action act = () => ProductServices.GetAllProduct(query);

            //Then
            Assert.StartsWith("MLB", query.Search);
            Assert.Equal(4, returnedProducts.quantityData);
            // var exception = Assert.Throws<ArgumentException>(act);
            // Assert.Equal("The MaxPrice value has to be greater than MiPrice value", exception.Message);
        }

        [Fact]
        public void GetProductsByCategory_throw_exception()
        {
            //Given
            var query = QueryGenerator(2, 11);
            query.MaxPrice = double.MinValue;

            //When
            Action act = () => ProductServices.GetAllProduct(query);

            //Then
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("The MaxPrice value has to be greater than MiPrice value", exception.Message);
        }

        [Fact]
        public void GetProductsByCategory_isThereAnyProductsInBD_returns_false()
        {
            //Given
            var product = ProductGenerator();
            var query = QueryGenerator(2, 11);
            ProductRepository.Setup(r => r.GetProductsByCategories(query)).Returns(ListProductGenerator());
            var sla = ProductRepository.Object.GetFilterProductsByName(query);
            sla.isThereAnyProductsInBD = false;
            ProductRepository.Setup(p => p.GetFilterProductsByName(query)).Returns(sla);

            //When
            var returnedProducts = ProductServices.GetAllProduct(query);

            //Then
            Assert.Equal(0, returnedProducts.quantityData);
        }     

        [Fact]
        public void GetProductsByCategory_isThereAnyProductsInBD_returns_true()
        {
            //Given
            var product = ProductGenerator();
            var query = QueryGenerator(2, 11);
            ProductRepository.Setup(r => r.GetProductsByCategories(query)).Returns(ListProductGenerator());
            
            var GetFilterProductsByNameReturn = ProductRepository.Object.GetFilterProductsByName(query);
            GetFilterProductsByNameReturn.isThereAnyProductsInBD = true;
            ProductRepository.Setup(p => p.GetFilterProductsByName(query)).Returns(GetFilterProductsByNameReturn);

            //When
            var returnedProducts = ProductServices.GetAllProduct(query);

            //Then
            Assert.Equal(0, returnedProducts.quantityData);
        }

        [Fact]
        public void GetProductsByUserPreferences_returns_list_of_product()
        {
            //Given
            ProductRepository.Setup(r => r.GetProductsByUserPreferences(It.IsAny<string>())).Returns(ListProductGenerator());
            
            //When
            var result = ProductServices.GetProdutsBasedOnUserSearches("search tag");

            //Then
            ProductRepository.Verify(r => r.GetProductsByUserPreferences("search tag"), Times.Once());
            Assert.NotNull(result);
        }

        [Fact]
        public void GetProductsByUserPreferences_doesnt_returns_list_of_product()
        {
            //Given, When
            var result = ProductServices.GetProdutsBasedOnUserSearches("search tag");

            //Then
            ProductRepository.Verify(r => r.GetProductsByUserPreferences("search tag"), Times.Once());
            Assert.Empty(result);
        }

        [Fact]
        public void GetRelatedProductsDTO_returns_list_of_products()
        {
            //Given
            ProductRepository.Setup(r => r.GetRelatedProducts(It.IsAny<Guid>())).Returns(new List<Product>{ProductGenerator()});
            
            //When
            var result = ProductServices.GetRelatedProductsDTO(Guid.NewGuid());
            
            //Then
            ProductRepository.Verify(r => r.GetRelatedProducts(It.IsAny<Guid>()), Times.Once());
            Assert.Single(result);
        }    

        [Fact]
        public void TestName()
        {
            //Given
            
            
            //When
            
            //Then
        }
    }
}