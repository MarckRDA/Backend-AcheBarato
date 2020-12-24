using System;
using Domain.Models.Users;

namespace Domain.src.Users
{
    public class UserServices
    {
        private IUserRepository _UserRepository = new UserRepository();

        public User CreateUser(string name, string password, string email )
        {
            var newuser = new User(name, password,email);
            if (newuser.Validate().isValid)
            {
                _UserRepository.AddUser(newuser);
                return newuser;
            }

            return null;
        }

        public User ObterUsuario(Guid idUser)
        {
            return _UserRepository.GetUser(idUser);
        }

        public (bool isValid, Guid id) AdicionarUsuario(string name, string password, string email)
        {
            var newuser = CreateUser(name,password,email);

            if (newuser == null)
            {
                return (false, Guid.Empty);
            }

            return (true, newuser.UserId);
        }

        public void RemoverUsuario(Guid idUser)
        {
            _UserRepository.RemoveUser(idUser);
        }
    }
}