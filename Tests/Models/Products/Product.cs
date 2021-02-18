using Domain.Models.Cathegories;
using Domain.Models.Descriptions;
using Domain.Models.HistorycalPrices;
using Domain.Models.Products;
using Moq;
using Tests.Mocks;
using Xunit;

namespace Tests.Products
{
    public class ProductTest : ProductsTestMethods
    {
        [Fact]
        public void AddDescription_is_valid()
        {
            //Given
            var cathegory = new Cathegory("MLA5725", "Accesorios para Vehiculos");
            var product = new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", cathegory, new string[1]{"tag"}
            );
            var description = new Description("Name", "Value");
            
            //When
            product.AddDescription(description);
            
            //Then
            var count = product.Descriptions.Count;
            const int actual = 1;
            Assert.Equal(count, actual);
        }

        [Fact]
        public void AddPicture_is_valid()
        {
            //Given
            var cathegory = new Cathegory("MLA5725", "Accesorios para Vehiculos");
            var product = new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", cathegory, new string[1]{"tag"}
            );
            var description = new Description("Name", "Value");
            
            //When
            product.AddPicture(description.Name);
            
            //Then
            var count = product.Pictures.Count;
            const int actual = 1;
            Assert.Equal(count, actual);
        }

        [Fact]
        public void AddHistoricalPrice_is_valid()
        {
            //Given
            var cathegory = new Cathegory("MLA5725", "Accesorios para Vehiculos");
            var product = new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", cathegory, new string[1]{"tag"}
            );
            
            //When
            product.AddHistoricalPrice(new HistorycalPrice(500, "02/06/2003"));
            
            //Then
            var count = product.HistorycalṔrices.Count;
            const int actual = 1;
            Assert.Equal(count, actual);
        }   

        [Fact]
        public void UpdateProductPrice_is_valid()
        {
            //Given
            var cathegory = new Cathegory("MLA5725", "Accesorios para Vehiculos");
            var product = new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", cathegory, new string[1]{"tag"}
            );
            var priceBeforeUpdate = product.Price;
            
            //When
            product.UpdateProductPrice(500);
            
            //Then
            Assert.True(priceBeforeUpdate != product.Price);
        }     

        [Fact]
        public void UpdateProductPrice_is_invalid()
        {
            //Given
            var cathegory = new Cathegory("MLA5725", "Accesorios para Vehiculos");
            var product = new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", cathegory, new string[1]{"tag"}
            );
            var priceBeforeUpdate = product.Price;
            
            //When
            product.UpdateProductPrice(100);
            
            //Then
            Assert.False(priceBeforeUpdate != product.Price);
        }                       
    }
}