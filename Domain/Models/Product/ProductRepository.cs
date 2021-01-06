using System;
using System.Collections.Generic;
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
            using (var db = new AcheBaratoContext())
            {
                var supposedProduct = db.Products.FirstOrDefault(pd => pd.ProductIdMLB == product.ProductIdMLB);
                if (supposedProduct == null)
                {
                    _productRepository.add(product);
                }
            }
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

        //Fazer uma maneira de pesquisar por keywords
        public List<Product> GetProducts(string search)
        {
            var searchSplited = search.Split(' ');
            
            using (var db = new AcheBaratoContext())
            {
                var filterTag = db.Tags.Where(tg => tg.Name == searchSplited[0]).ToList();
                
                var products = new List<Product>();

                foreach (var tag in filterTag)
                {
                    var p = db.Products.FirstOrDefault(pd => pd.Tags.Contains(tag));

                    products.Add(p);
                }

                return products;
            }
            

            
        }

    }
}