using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Attributes;
using Domain.Common;
using MongoDB.Driver;

namespace Infra.Repository
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IMongoSettings settings)
        {
            var database = new MongoClient("mongodb://root:AcheBaratoMongoDB2021!@localhost:27017").GetDatabase("AcheBarato");
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

        public TEntity GetEntityById(Expression<Func<TEntity,Guid>> function, Guid value)
        {
            var filter = Builders<TEntity>.Filter.Eq(function, value);
            return _collection.Find(filter).FirstOrDefault();
        }
        
    }

}