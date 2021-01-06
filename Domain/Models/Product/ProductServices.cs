using System.Collections.Generic;
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
            var products = _productRepository.GetProducts(search);
            var listProducts = new List<ProductDTO>();
            
            foreach (var productsInDB in products)
            {
                var productToInsertInDB = new ProductDTO()
                {
                    Name = productsInDB.Name,
                    Price = productsInDB.Price,
                    ImgLink = productsInDB.ThumbImgLink,
                };

                listProducts.Add(productToInsertInDB);  
            }

            return listProducts;
        }

        public ProductDTO GetProductDTO(string idMLBProduct)
        {
            var productFound = _productRepository.GetElement(product => product.ProductIdMLB == idMLBProduct);

            return new ProductDTO()
            {
                Name = productFound.Name,
                Price = productFound.Price,
                Descriptions = productFound.Descriptions,
                HistorycalPrices = productFound.HistorycalPrices,
                LinkRedirectShop = productFound.LinkRedirectShop,
                Pictures = productFound.Pictures
            };
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