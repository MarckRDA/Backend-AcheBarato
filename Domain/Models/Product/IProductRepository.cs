using Domain.Infra;

namespace Domain.Models.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        void UpdateProductPrice(string idMLB, double newPrice);
        void UpdateHistoricalPrice(string idMLB, HistorycalPrice historicalprice);
        
    }
}