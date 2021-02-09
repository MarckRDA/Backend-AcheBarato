using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.Cathegories;


namespace Domain.Models.Products
{
    public interface IProductServices
    {
        IQueryable<ProductDTO> GetAllProduct(ProductQueryParameters parameters);
        ProductDTO GetProductDTOById(Guid idProduct);
        IEnumerable<ProductDTO> GetTrendProductsDTO();
        List<ProductDTO> GetRelatedProductsDTO(Guid idProduct);
        List<Cathegory> GetCathegories();       
        List<Product> GetProdutsBasedOnUserSearches(string searchTag);

    }
}