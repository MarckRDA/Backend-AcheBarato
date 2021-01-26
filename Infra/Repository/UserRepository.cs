using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Common;
using Domain.Models.Users;
using MongoDB.Driver;

namespace Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;
        private readonly IMongoSettings _settings;
        private readonly IMongoCollection<User> _collection;



        public UserRepository(IMongoRepository<User> repository, IMongoSettings settings)
        {
            _repository = repository;
             _settings = settings;
            var database = new MongoClient("mongodb://root:AcheBaratoMongoDB2021!@localhost:27017").GetDatabase("AcheBarato");
            _collection = database.GetCollection<User>("Users");
        }

        public void add(User entity)
        {
            _repository.add(entity);
        }

        public IEnumerable<User> GetAllElements()
        {
            return _repository.GetAllElements();
        }

        public User GetEntityById(Expression<Func<User, Guid>> function, Guid value)
        {
            return _repository.GetEntityById(function, value);
        }
        public void UpdateUserInformations(User userToUpdate)
        {
           _collection.ReplaceOne(user => user.Id == userToUpdate.Id, userToUpdate);
        }
        public User GetUserByEmail(string userEmail)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Email, userEmail);
            return _collection.Find(filter).FirstOrDefault();

        }
        
    }
}