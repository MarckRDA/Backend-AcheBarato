using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ApiMLBConnection.Consumers;

namespace Domain.Models.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public List<ProductDTO> GetAllProduct(string search)
        {
            PostProductInDB(search);
            var products = _productRepository.GetElement(product => product.Tags.Contains(search.ToUpper()));
            var listProducts = new List<ProductDTO>();
            
            foreach (var productsInDB in listProducts)
            {
                var productToInsertInDB = new ProductDTO()
                {
                    Name = productsInDB.Name,
                    Price = productsInDB.Price,
                    ImgLink = productsInDB.ImgLink,
                };  
            }

            return listProducts;
        }

        public ProductDTO GetProductDTO(Guid idProduct)
        {
            throw new NotImplementedException();
        }

        private void PostProductInDB(string search)
        {
            var products = ApiMLB.GetProducts(search);

            foreach (var pd in products)
            {
                _productRepository.add(pd);
            }

        }
    }

}