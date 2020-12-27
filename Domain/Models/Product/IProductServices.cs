using System;
using System.Collections.Generic;

namespace Domain.Models.Products
{
    public interface IProductServices
    {
        ProductDTO GetProductDTO(string idMLBProduct);
        List<ProductDTO> GetAllProduct(string search);
    }
}