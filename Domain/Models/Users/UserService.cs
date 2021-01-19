using System;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;    
        }

        public (bool isValid, Guid id) AddUser(string name, string password, string email)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(string name, string password, string email)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid idUser)
        {
            return _repository.GetEntityById(x => x.Id, idUser);
        }
    }
}