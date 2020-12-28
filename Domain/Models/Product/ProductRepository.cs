using System;
using System.Linq;
using Domain.Infra;

namespace Domain.Models.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<Product> _productRepository;

        public ProductRepository(IRepository<Product> repository)
        {
            _productRepository = repository;
        }

        //irá ser utilizado para o sistema de atualização automatica.
        public void UpdateProductPrice(string idMLB, double newPrice)
        {    
            using (var db = new AcheBaratoContext())
            {
                var productToUpdate = db.Products.FirstOrDefault(product => product.ProductIdMLB == idMLB);

                productToUpdate.Price = newPrice;

                db.SaveChanges();
            }
        }
        
        public void add(Product product)
        {
            if (GetElement(product => product.ProductIdMLB == product.ProductIdMLB) == null)
            {
                return;
            } 
            _productRepository.add(product);
        }

        public Product GetElement(Func<Product, bool> predicate)
        {

            return _productRepository.GetElement(predicate);
        }

        public void UpdateHistoricalPrice(string idMLB, HistorycalPrice historicalprice)
        {
            using (var db = new AcheBaratoContext())
            {
                var productToUpdate = db.Products.FirstOrDefault(product => product.ProductIdMLB == idMLB);

                productToUpdate.HistorycalPrices.Add(historicalprice);

                db.SaveChanges();
            }           
        }

        
    }
}