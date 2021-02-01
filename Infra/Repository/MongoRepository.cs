using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Attributes;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Repository
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly IConfiguration _configuration;

        public MongoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var database = new MongoClient(_configuration.GetValue<string>("MongoSettings:Connection")).GetDatabase(_configuration.GetValue<string>("MongoSettings:DatabaseName"));
            _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual void add(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public virtual IEnumerable<TEntity> GetAllElements()
        {
            return _collection.AsQueryable().ToList();
        }

        public TEntity GetEntityById(Expression<Func<TEntity, Guid>> function, Guid value)
        {
            var filter = Builders<TEntity>.Filter.Eq(function, value);
            return _collection.Find(filter).FirstOrDefault();
        }

    }

}