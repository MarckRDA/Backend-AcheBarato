using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ApiMLBConnection.Consumers;
using Domain.Models.Cathegories;

namespace Domain.Models.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repository;

        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }


        public IQueryable<Product> GetAllProduct(ProductQueryParameters parameters)
        {
            var productInDB = _repository.GetFilterProductsByName(parameters);
            
            if (!productInDB.isThereAnyProductsInBD)
            {
                PostProductInDB(parameters.Search);
                return _repository.GetFilterProductsByName(parameters).products;
            }

            return productInDB.products;
         
        }

        public List<Product> GetProductsByCategory(string category)
        {
            return _repository.GetProductsByCategories(category);
        }

        public List<Cathegory> GetCathegories()
        {
            return _repository.GetCathegories();
        }

        public ProductDTO GetProductDTOById(Guid idProduct)
        {
            var gotProductFromDB = _repository.GetEntityById(x => x.id_product, idProduct);
            
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