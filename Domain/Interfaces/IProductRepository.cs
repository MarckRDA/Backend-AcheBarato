using System;
using System.Collections.Generic;
using Domain.Models.Products;

namespace Domain.Interfaces
{
    public interface IProductRepository : IMongoRepository<Product>
    {
        IEnumerable<Product> GetFilterProductsByName(string search);
        Product GetProductById(Guid idProduct);
        
    }
}