using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Models.Cathegories;

namespace Domain.Models.Products
{
    public interface IProductRepository : IMongoRepository<Product>
    {
        (IQueryable<Product> products, bool isThereAnyProductsInBD) GetFilterProductsByName(ProductQueryParameters search);

        
        void AddManyProductsAtOnce(List<Product> products);

        List<Cathegory> GetCathegories();

        List<Product> GetProductsByCategories(string category);

    }
}