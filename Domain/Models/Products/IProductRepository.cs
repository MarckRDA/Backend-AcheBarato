using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Models.Cathegories;

namespace Domain.Models.Products
{
    public interface IProductRepository : IMongoRepository<Product>
    {
        (IQueryable<Product> products, bool isThereAnyProductsInBD, int quantitySerached) GetFilterProductsByName(QueryParameters search);
        Product GetProductByMLBId(string MLBId);
        List<Product> GetRelatedProducts(Guid idProduct);
        IEnumerable<Product> GetProductsByUserPreferences(string searchTag);

        IEnumerable<Product> GetTrendProducts();
        Task AddManyProductsAtOnce(List<Product> products);

        List<Cathegory> GetCathegories();

        List<Product> GetProductsByCategories(QueryParameters parameters);
        List<Product> GetProductsByCategories(string IdMLB);

    }
}