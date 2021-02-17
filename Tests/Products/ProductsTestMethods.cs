using System.Collections.Generic;
using Domain.Common;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Moq;
using Tests.Mocks;

namespace Tests.Products
{
    public abstract class ProductsTestMethods : MyMocks
    {
        protected QueryParameters QueryGenerator(int pageNumber, int limit)
        {
            return new QueryParameters(pageNumber, limit);
        }

        protected Cathegory CathegoryGenerator()
        {
            return new Cathegory("MLA5725", "Accesorios para Vehiculos");
        }

        protected Product ProductGenerator()
        {
            return new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", CathegoryGenerator(), new string[1]{"tag"}
            );
        }

        protected List<Product> ListProductGenerator()
        {
            return new List<Product>{
                ProductGenerator(),ProductGenerator(),ProductGenerator(),ProductGenerator()
            };
        }

        protected Mock<IMongoRepository<Product>> MockedMongoDBGenerator()
        {
            return new Mock<IMongoRepository<Product>>();
        }
    }
}