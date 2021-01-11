using System;
using System.Collections.Generic;
using Domain.Models.Products;

namespace Domain.Interfaces
{
    public interface IProductRepository : IMongoRepository<Product>
    {
        (IEnumerable<Product> products, bool isThereAnyProductsInBD) GetFilterProductsByName(string search);

        Product GetProductById(Guid idProduct);
        
        void AddManyProductsAtOnce(List<Product> products);

        List<string> GetCathegories();
    }
}