using Moq;
using Domain.Common;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Tests.Mocks;
using Xunit;
using Moq.Protected;
using System.Collections.Generic;

namespace Tests.Products
{
    public class ProductsServicesTest : MyMocks
    {
        public QueryParameters QueryGenerator(int pageNumber, int limit)
        {
            return new QueryParameters(pageNumber, limit);
        }

        public Cathegory CathegoryGenerator()
        {
            return new Cathegory("MLA5725", "Accesorios para Vehiculos");
        }

        public Product ProductGenerator()
        {
            return new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", CathegoryGenerator(), new string[1]{"tag"}
            );
        }

        public List<Product> ListProductGenerator()
        {
            return new List<Product>{
                ProductGenerator(),ProductGenerator(),ProductGenerator(),ProductGenerator()
            };
        }

        [Fact]
        public void GetProductsByCategory_is_valid()
        {
            //Given
            var product = ProductGenerator();
            var query = QueryGenerator(2, 11);
            query.Search = "MLB";
            ProductRepository
                .Setup(p => p.GetProductsByCategories(query))
                .Returns(ListProductGenerator());

            //When
            var expected = MockProductServices.Object.GetAllProduct(query);

            //Then
        }
    }
}