using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Domain.Products;

namespace Domain.Interfaces
{
    public interface IProductServices
    {
        IQueryable<Product> GetAllProduct(ProductQueryParameters parameters);
        ProductDTO GetProductDTOById(Guid idProduct);

        List<Cathegory> GetCathegories();       

        List<Product> GetProductsByCategory(string category);

    }
}