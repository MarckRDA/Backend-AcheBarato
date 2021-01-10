using System;
using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Interfaces
{
    public interface IUserServices
    {
        User CreateUser(string name, string password, string email );
        Task<User> ObterUsuario(Guid idUser);
        (bool isValid, Guid id) AdicionarUsuario(string name, string password, string email);
        
    }
}