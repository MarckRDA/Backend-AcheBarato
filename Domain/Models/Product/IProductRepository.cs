using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Models.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProducts(string search);
        void UpdateProductPrice(string idMLB, double newPrice);
        void UpdateHistoricalPrice(string idMLB, HistorycalPrice historicalprice);
        
    }
}