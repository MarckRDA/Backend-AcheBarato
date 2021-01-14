using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Common;
using Domain.Models.Users;

namespace Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;

        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
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
    }
}