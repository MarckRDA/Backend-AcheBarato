using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models.Products;
using MongoDB.Driver;

namespace Domain.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IMongoSettings _settings;
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoRepository<Product> repository, IMongoSettings settings)
        {
            _repository = repository;
            _settings = settings;
            var database = new MongoClient("mongodb://root:AcheBaratoMongoDB2021!@localhost:27017").GetDatabase("AcheBarato");
            _collection = database.GetCollection<Product>("Products");
        }

        public void add(Product entity)
        {
            _repository.add(entity);
        }

        public IEnumerable<Product> GetAllElements()
        {
            return _repository.GetAllElements();
        }

        public IEnumerable<Product> GetFilterProductsByName(string search)
        {
            var splitedSearch = search.ToUpper().Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var filter = Builders<Product>.Filter.All(x => x.Tag, splitedSearch);
            var p =_collection.Find(filter).ToList();
            return p;
        }

        public Product GetProductById(Guid idProduct)
        {
            var filterProduct = Builders<Product>.Filter.Eq(x => x.Id, idProduct.ToString());
            return _collection.Find(filterProduct).FirstOrDefault();
        }
            
    }
}