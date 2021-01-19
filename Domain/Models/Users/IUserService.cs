using System;

namespace Domain.Models.Users
{
    public interface IUserService
    {
        User CreateUser(string name, string password, string email );
        User GetUser(Guid idUser);
        (bool isValid, Guid id) AddUser(string name, string password, string email);
        
    }
}