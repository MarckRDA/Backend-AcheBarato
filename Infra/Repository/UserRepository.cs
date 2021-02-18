using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Common;
using Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;
        private readonly IMongoCollection<User> _collection;
        private readonly IConfiguration _configuration;
        
        public UserRepository(IMongoRepository<User> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
           var database = new MongoClient(_configuration.GetValue<string>("MongoSettings:Connection")).GetDatabase(_configuration.GetValue<string>("MongoSettings:DatabaseName"));
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

        public bool ExistAnyUserWithThisEmail(string email)
        {
            return GetUserByEmail(email) != null;
        }
    }
}