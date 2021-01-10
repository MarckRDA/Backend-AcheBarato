using System;
using System.Collections.Generic;
using Domain.Models.Products;

namespace Domain.Interfaces
{
    public interface IProductServices
    {
        IEnumerable<Product> GetAllProduct(string search);
        ProductDTO GetProductDTOById(Guid idProduct);

        
    }
}