using System;
using Domain.Models.Users;

namespace Domain.src.Users
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;

        public UserServices(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public User CreateUser(string name, string password, string email )
        {
            var newuser = new User(name, password,email);
            if (newuser.Validate().isValid)
            {
                _userRepository.add(newuser);
                return newuser;
            }

            return null;
        }

        public User ObterUsuario(Guid idUser)
        {
            return _userRepository.GetElement(user => user.UserId == idUser);
        }

        [Obsolete("What the fuck is it, vinicius?")]
        public (bool isValid, Guid id) AdicionarUsuario(string name, string password, string email)
        {
            var newuser = CreateUser(name,password,email);

            if (newuser == null)
            {
                return (false, Guid.Empty);
            }

            return (true, newuser.UserId);
        }

        
    }
}