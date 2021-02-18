using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Models.Cathegories;
using Domain.Models.Descriptions;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IMongoCollection<Product> _collection;
        private readonly IConfiguration _configuration;

        public ProductRepository(IMongoRepository<Product> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            var database = new MongoClient(_configuration.GetValue<string>("MongoSettings:Connection"))
                .GetDatabase(_configuration.GetValue<string>("MongoSettings:DatabaseName"));
            _collection = database.GetCollection<Product>("Products");
        }

        public void add(Product entity)
        {
            _repository.add(entity);
        }

        public async Task AddManyProductsAtOnce(List<Product> products)
        {
            await _collection.InsertManyAsync(products);
        }

        public IEnumerable<Product> GetAllElements()
        {
            return _repository.GetAllElements();
        }

        public IEnumerable<Product> GetProductsByUserPreferences(string searchTag)
        {
            return _collection.Find(x => x.Name.Contains(searchTag)).ToEnumerable().Take(10);
        }

        public IEnumerable<Product> GetTrendProducts()
        {
            var filter = Builders<Product>.Filter;
            List<Product> trendProducts = new List<Product>();

            var filterTrendConsoles = filter.Eq(x => x.Cathegory.Name, "Consoles");
            var filterTrendGeladeiras = filter.Eq(x => x.Cathegory.Name, "Geladeiras");
            var filterTrendCelulares = filter.Eq(x => x.Cathegory.Name, "Celulares e Smartphones");
            var filterTrendArCondicionado = filter.Eq(x => x.Cathegory.Name, "Ar Condicionado");
            
            foreach (var product in _collection.Find(filterTrendConsoles).ToList().TakeLast(10))
            {
                trendProducts.Add(product);
            }

            foreach (var product in _collection.Find(filterTrendGeladeiras).ToList().TakeLast(10))
            {
                trendProducts.Add(product);
            }

            foreach (var product in _collection.Find(filterTrendCelulares).ToList().TakeLast(10))
            {
                trendProducts.Add(product);
            }
            
            foreach (var product in _collection.Find(filterTrendArCondicionado).ToList().TakeLast(10))
            {
                trendProducts.Add(product);
            }

            return trendProducts;
        }

        public List<Cathegory> GetCathegories()
        {
            return _collection.AsQueryable().Select(x => x.Cathegory).Distinct().ToList();
        }

        public List<Product> GetRelatedProducts(Guid idProduct)
        {
            var productToBasedOnItsCategory = GetEntityById(pd => pd.id_product, idProduct);
            return GetProductsByCategories(productToBasedOnItsCategory.Cathegory.IdMLB).Take(10).ToList();
        }

        public (IQueryable<Product> products, bool isThereAnyProductsInBD, int quantitySerached) GetFilterProductsByName(QueryParameters parameters)
        {
            var splitedSearch = parameters.Search
                .ToUpper()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            var builder = Builders<Product>.Filter;
            
            var filterTag = builder.All(x => x.Tag, splitedSearch) 
                & builder.Gte(productsSearched => productsSearched.Price, parameters.MinPrice) 
                & builder.Lte(p => p.Price, parameters.MaxPrice);
            
            IQueryable<Product> productsSearched =  _collection
                .Find(filterTag)
                .ToList()
                .AsQueryable();

            var orderingFilter = parameters.OrderBy; 
            
            if (orderingFilter == "max")
            {
                productsSearched =  _collection.Find(filterTag)
                    .SortByDescending(x => x.Price)
                    .ToList()
                    .AsQueryable();
            }
            else if (orderingFilter == "min")
            {
                productsSearched =  _collection.Find(filterTag)
                    .SortBy(x => x.Price)
                    .ToList()
                    .AsQueryable();

            }
            else if (orderingFilter == "az")
            {
                productsSearched =  _collection.Find(filterTag)
                    .SortBy(x => x.Name)
                    .ToList()
                    .AsQueryable();
            }
            else if (orderingFilter == "za")
            {
                productsSearched =  _collection.Find(filterTag)
                    .SortByDescending(x => x.Name)
                    .ToList()
                    .AsQueryable();
            }

            return (productsSearched.Skip((parameters.PageNumber - 1) * parameters.Limit)
                .Take(parameters.Limit), 
                productsSearched.Count() > 10, 
                productsSearched.Count());
        }

        public List<Product> GetProductsByCategories(QueryParameters parameters)
        {
            return _collection.AsQueryable().Where(x => x.Cathegory.IdMLB == parameters.Search)
                .Skip((parameters.PageNumber - 1) * parameters.Limit)
                .Take(parameters.Limit)
                .ToList();
        }
        public List<Product> GetProductsByCategories(string IdMLB)
        {
            return _collection.AsQueryable().Where(x => x.Cathegory.IdMLB == IdMLB).ToList();
        }

        public List<Description> GetProductDescription(Guid idProduct)
        {
            return GetEntityById(x => x.id_product, idProduct).Descriptions;
        }

        public Product GetEntityById(Expression<Func<Product, Guid>> function, Guid value)
        {
            return _repository.GetEntityById(function, value);
        }

        public Product GetProductByMLBId(string MLBId)
        {
            return _collection.Find(x => x.MLBId == MLBId).FirstOrDefault();
        }

    }
}