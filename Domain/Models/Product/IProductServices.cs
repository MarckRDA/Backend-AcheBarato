using System;
using System.Collections.Generic;

namespace Domain.Models.Products
{
    public interface IProductServices
    {
        ProductDTO GetProductDTO(Guid idProduct);
        List<ProductDTO> GetAllProduct(string search);
    }
}