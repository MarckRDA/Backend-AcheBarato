using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Models.Cathegories;


namespace Domain.Models.Products
{
    public interface IProductServices
    {
        (IQueryable<ProductDTO> productsSeached, int quantityData) GetAllProduct(QueryParameters parameters);
        ProductDTO GetProductDTOById(Guid idProduct);
        IEnumerable<ProductDTO> GetTrendProductsDTO();
        List<ProductDTO> GetRelatedProductsDTO(Guid idProduct);
        List<Cathegory> GetCathegories();       
        List<Product> GetProdutsBasedOnUserSearches(string searchTag);

    }
}