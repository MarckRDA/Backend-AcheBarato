using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.Cathegories;


namespace Domain.Models.Products
{
    public interface IProductServices
    {
        IQueryable<Product> GetAllProduct(ProductQueryParameters parameters);
        ProductDTO GetProductDTOById(Guid idProduct);
        IEnumerable<ProductDTO> GetTrendProductsDTO();
        List<Cathegory> GetCathegories();       

        List<Product> GetProductsByCategory(string category);

    }
}