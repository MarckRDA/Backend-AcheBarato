using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ApiMLBConnection.Consumers;
using Domain.Common;
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

        public (IQueryable<ProductDTO> productsSeached, int quantityData) GetAllProduct(QueryParameters parameters)
        {
            if (parameters.Search.StartsWith("MLB"))
            {
                var products = GetProductsByCategory(parameters); 
                return (products.products1.Select(product => new ProductDTO()
                {
                    Name = product.Name,
                    ThumbImgLink = product.ThumbImgLink,
                    Price = product.Price,
                    id_product = product.id_product

                }).AsQueryable(), products.quantityData);
            }

            if(!parameters.ValidateValuePrice())
            {
                throw new ArgumentException("The MaxPrice value has to be greater than MiPrice value");
            }
            
            var productInDB = _repository.GetFilterProductsByName(parameters);
            if (!productInDB.isThereAnyProductsInBD)
            {
                PostProductInDB(parameters.Search);
                var searched = _repository.GetFilterProductsByName(parameters);
                var p = searched.products;
                var total = searched.quantitySerached;
  
                return (p.Select(product => new ProductDTO()
                {
                    Name = product.Name,
                    ThumbImgLink = product.ThumbImgLink,
                    Price = product.Price,
                    id_product = product.id_product
                }), total);
            }

            return (productInDB.products.Select(product => new ProductDTO()
                {
                    Name = product.Name,
                    ThumbImgLink = product.ThumbImgLink,
                    Price = product.Price,
                    id_product = product.id_product

                }), productInDB.quantitySerached);
        }

        public List<Product> GetProdutsBasedOnUserSearches(string searchTag)
        {
            return _repository.GetProductsByUserPreferences(searchTag).ToList();
        } 

        private (List<Product> products1, int quantityData) GetProductsByCategory(QueryParameters parameters)
        {
            var products = _repository.GetProductsByCategories(parameters);
            return (products, products.Count);
        }
        
        public List<ProductDTO> GetRelatedProductsDTO(Guid idProduct)
        {
            return _repository
                .GetRelatedProducts(idProduct)
                .Select(relatedproduct => new ProductDTO
                {
                    Name = relatedproduct.Name,
                    id_product = relatedproduct.id_product,
                    ThumbImgLink = relatedproduct.ThumbImgLink,
                    LinkRedirectShop = relatedproduct.LinkRedirectShop,
                    Price = relatedproduct.Price,
                })
                .ToList();
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
                id_product = gotProductFromDB.id_product,
                Descriptions = gotProductFromDB.Descriptions,
                ThumbImgLink = gotProductFromDB.ThumbImgLink,
                Pictures = gotProductFromDB.Pictures,
                LinkRedirectShop = gotProductFromDB.LinkRedirectShop,
                HistorycalṔrices = gotProductFromDB.HistorycalṔrices,
                Price = gotProductFromDB.Price,
            };
        }

        private void PostProductInDB(string search)
        {
            var products = ApiMLB.GetProducts(search);
            // foreach (var product in products)
            // {
            //     if (_repository.GetProductByMLBId(product.MLBId) != null)
            //     {
            //         products.Remove(product);
            //         continue;
            //     }
            // }

            // if (products == null)
            // {
            //     return;
            // }
            _repository.AddManyProductsAtOnce(products);
        }

        public IEnumerable<ProductDTO> GetTrendProductsDTO()
        {
            var productsToProductsDTO = _repository.GetTrendProducts();
            var trendsProductsDTO = new List<ProductDTO>();
            
            foreach (var product in productsToProductsDTO)
            {
                trendsProductsDTO.Add(new ProductDTO()
                {
                    Name = product.Name,
                    id_product = product.id_product,
                    Cathegory = product.Cathegory,
                    Price = product.Price,
                    ThumbImgLink = product.ThumbImgLink
                });
            }

            return trendsProductsDTO;
        }
    }
}