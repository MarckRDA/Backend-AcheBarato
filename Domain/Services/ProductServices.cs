using System;
using System.Collections.Generic;
using Domain.ApiMLBConnection.Consumers;
using Domain.Interfaces;
using Domain.Models.Products;

namespace Domain.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repository;

        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Product> GetAllProduct(string search)
        {
            var productInDB = _repository.GetFilterProductsByName(search);
            
            if (!productInDB.isThereAnyProductsInBD)
            {
                PostProductInDB(search);
                return _repository.GetFilterProductsByName(search).products;
            }

            return productInDB.products;
         
        }

        public ProductDTO GetProductDTOById(Guid idProduct)
        {
            var gotProductFromDB = _repository.GetProductById(idProduct);
            
            return new ProductDTO()
            {
                Name = gotProductFromDB.Name,
                Descriptions = gotProductFromDB.Descriptions,
                ImgLink = gotProductFromDB.ThumbImgLink,
                Pictures = gotProductFromDB.Pictures,
                LinkRedirectShop = gotProductFromDB.LinkRedirectShop,
                HistorycalPrices = gotProductFromDB.HistorycalṔrices,
                Price = gotProductFromDB.Price,
                Cathegory = gotProductFromDB.Cathegory

            };
        }

        private void PostProductInDB(string search)
        {
            var products = ApiMLB.GetProducts(search);

            _repository.AddManyProductsAtOnce(products);
            
        }
    }

}